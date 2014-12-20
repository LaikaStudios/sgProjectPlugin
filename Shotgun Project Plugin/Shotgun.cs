using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.ComponentModel;

namespace sg_prj
{
    class Shotgun {
        private static readonly Lazy<Shotgun> _instance = new Lazy<Shotgun>(() => new Shotgun(), true);
        private static ScriptScope _pythonModule = null;

        private Shotgun() {
            lock (this) {
                if (_pythonModule == null) {
                    // Find out where our files are
                    Assembly exAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    Uri uriCodeBase = new Uri(exAssembly.CodeBase);
                    String installBase = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString());
                    // Initialize IronPython
                    ScriptRuntime ipy = Python.CreateRuntime();
                    IronPython.Runtime.List path = ipy.GetSysModule().GetVariable("path");
                    // Point IronPython to our files
                    path.append(Path.Combine(installBase, "Python", "PythonStatic.zip"));
                    _pythonModule = ipy.UseFile(Path.Combine(installBase, "Python", "shotgun_config.py"));
                }
            }
        }

        public static Shotgun Instance {
            get { return _instance.Value; }
        }

        public dynamic connect() {
            if (Properties.Settings.Default.ShotgunProject == 0)
                setDefaultProject();
            return _pythonModule.GetVariable("connect")(Properties.Settings.Default.ShotgunInstance);
        }

        public void setDefaultProject() {
            int row;
            // Setup the dialog
            ShotgunProjectsForm form = new ShotgunProjectsForm();
            DataGridViewRowCollection rows = form.projectsGrid.Rows;
            rows.Clear();
            dynamic projects = _pythonModule.GetVariable("projects")();
            int selectedRow = -1;
            Hashtable rowToId = new Hashtable();
            Hashtable rowToInst = new Hashtable();
            Hashtable rowToName = new Hashtable();
            foreach (dynamic project in projects) {
                if (project["name"] == "Template Project")
                    continue;
                row = rows.Add(project["name"]);
                if (project["id"] == Properties.Settings.Default.ShotgunProject)
                    selectedRow = row;
                rowToId[row] = project["id"];
                rowToInst[row] = project["inst"]["name"];
                rowToName[row] = project["name"];
            }
            if (selectedRow != -1)
                form.projectsGrid.CurrentCell = rows[selectedRow].Cells[0];
            // Show the dialog
            System.Windows.Forms.DialogResult result = form.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            row = form.projectsGrid.SelectedRows[0].Index;
            Properties.Settings.Default.ShotgunProject = (int)rowToId[row];
            Properties.Settings.Default.ShotgunInstance = (String)rowToInst[row];
            Properties.Settings.Default.ShotgunProjectName = (String)rowToName[row];
            Properties.Settings.Default.Save();
            MessageBox.Show("Project set to \"" + (String)rowToName[row] + "\".");
        }

        public PythonDictionary getTasks(BackgroundWorker bw, DoWorkEventArgs e) {
            
            List conds = new List() {
                new List() {"project", "is", new PythonDictionary() {{"type","Project"}, {"id",Properties.Settings.Default.ShotgunProject}}},
                new List() {"sg_status_list", "is_not", "omt"},
                new List() {"entity", "type_is", "Shot"},
                new List() {"entity.Shot.sg_status_list", "is_not", "omt"},
                new List() {"step.Step.code", "is", "Stage"},
                new List() {"entity.Shot.sg_shot_type", "is", "Scene"},
                new List() {"sg_task_type", "is", "YourProjectSyncableTaskType"},
            };
            // NOTE! sg_set_builds are the SETS, while sg_set_phases are the SET BUILDS!  ancient history... can't rename db fields in Shotgun...
            List fields = new List() { "content", "start_date", "due_date", "duration", "sg_description", "entity",
                "sg_set_builds", "sg_puppets", "sg_status_list", "sg_unit", "sg_set_phases",
                "entity.Shot.sg_cut_duration", "entity.Shot.parent_shots", "entity.Shot.sg_sequence", "sg_task_mode"};
            Func<PythonDictionary, String> hashKey = delegate(PythonDictionary dict) {
                return dict["content"] + " --++-- " + (dict["entity"] as PythonDictionary)["name"];
            };
            PythonDictionary allTasks = _sgGet("Task", conds, fields, bw, e, keyFunc: hashKey);
            PythonDictionary ret = new PythonDictionary();
            Regex inValidTasksEx = new Regex(@"(clpt.*)|(bg.*)", RegexOptions.IgnoreCase);
            foreach (KeyValuePair<object, object> taskEntry in allTasks) {
                PythonDictionary task = (PythonDictionary)taskEntry.Value;
                if (!inValidTasksEx.IsMatch((string)task["content"]))
                {
                    if (task["start_date"] != IronPython.Modules.Builtin.None)
                        task["start_date"] = DateTime.ParseExact(task["start_date"] as String, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    if (task["due_date"] != IronPython.Modules.Builtin.None)
                        task["due_date"] = DateTime.ParseExact(task["due_date"] as String, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    // Save this task in the results list
                    ret[taskEntry.Key] = task;
                }
            }
            return ret;
        }

        public PythonDictionary getUnits(BackgroundWorker bw, DoWorkEventArgs e) {
            List conds = new List() {
                new List() {"project", "is", new PythonDictionary() {{"type","Project"}, {"id",Properties.Settings.Default.ShotgunProject}}},
                new List() {"sg_type", "is", "Unit"},
                new List() {"sg_status_list", "is_not", "clsd"}
            };
            List fields = new List() { "code" };
            return _sgGet("PhysicalAsset", conds, fields, bw, e);
        }

        public PythonDictionary getSets(BackgroundWorker bw, DoWorkEventArgs e) {
            List conds = new List() {
                new List() {"project", "is", new PythonDictionary() {{"type","Project"}, {"id",Properties.Settings.Default.ShotgunProject}}},
                new List() {"sg_asset_type", "is", "Set"},
                new List() {"sg_status_list", "is_not", "omt"},
            };
            List fields = new List() { "code" };
            return _sgGet("Asset", conds, fields, bw, e);
        }

        public PythonDictionary getSetBuilds(BackgroundWorker bw, DoWorkEventArgs e)
        {
            List conds = new List() {
                new List() {"project", "is", new PythonDictionary() {{"type","Project"}, {"id",Properties.Settings.Default.ShotgunProject}}},
                new List() {"sg_type", "is", "Set Build"},
                new List() {"sg_status_list", "is_not", "omt"},
            };
            List fields = new List() { "code" };
            return _sgGet("CustomEntity18", conds, fields, bw, e);
        }

        public PythonDictionary getPuppets(BackgroundWorker bw, DoWorkEventArgs e) {
            List conds = new List() {
                new List() {"project", "is", new PythonDictionary() {{"type","Project"}, {"id",Properties.Settings.Default.ShotgunProject}}},
                new List() {"sg_asset_type", "is", "Puppet"},
                new List() {"sg_status_list", "is_not", "omt"},
            };
            List fields = new List() { "code" };
            PythonDictionary characterMap = _sgGet("Asset", conds, fields, bw, e);

            // Strip off the common prefix on all puppets "Puppet "
            String prefix = "Puppet ";
            PythonDictionary ret = new PythonDictionary();
            foreach (System.Collections.Generic.KeyValuePair<object,object> item in characterMap)
            {
                String puppetName = (String)item.Key;
                if (puppetName.StartsWith(prefix))
                {
                    puppetName = puppetName.Substring(prefix.Length - 1).Trim();
                }
                ret[puppetName] = item.Value;
            }
            return ret;
        }

        public PythonDictionary getShots(BackgroundWorker bw, DoWorkEventArgs e) {
            List conds = new List() {
                new List() {"project", "is", new PythonDictionary() {{"type","Project"}, {"id",Properties.Settings.Default.ShotgunProject}}},
                new List() {"sg_status_list", "is_not", "omt"},
            };
            List fields = new List() { "code", "sg_sets", "sg_characters" };
            return _sgGet("Shot", conds, fields, bw, e);
        }

        public PythonDictionary getStageStep(BackgroundWorker bw, DoWorkEventArgs e) {
            dynamic connection = connect();
            List conds = new List() {
                new List() {"entity_type", "is", "Shot"},
                new List() {"code", "is", "Stage"},
            };
            List fields = new List() { "code" };
            PythonDictionary step = connection.find_one("Step", conds, fields: fields);
            return step;
        }

        public PythonDictionary _sgGet(String eType, List conds, List fields, BackgroundWorker bw, DoWorkEventArgs e, Func<PythonDictionary, String> keyFunc = null) {
            if (keyFunc == null) {
                keyFunc = delegate(PythonDictionary dict) { return dict["code"] as String; };
            }
            dynamic connection = connect();
            int page = 1;
            List accum = new List();
            List pageObjs = new List();
            if (bw.CancellationPending) {
                e.Cancel = true;
                return null;
            }
            do {
                pageObjs = connection.find(eType, conds, fields: fields, limit: 100, page: page++);
                accum.extend(pageObjs);
                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return null;
                }
                bw.ReportProgress(0, accum.Count);
            } while (pageObjs.Count > 0);
            PythonDictionary ret = new PythonDictionary();
            foreach (PythonDictionary obj in accum)
                ret[keyFunc(obj) as String] = obj;
            return ret;
        }

        public void updateTasks(List creates, List updates) {
            List batch = new List();
            foreach (PythonDictionary task in creates) {
                task["project"] = new PythonDictionary() { { "type", "Project" }, { "id", Properties.Settings.Default.ShotgunProject } };
                batch.append(new PythonDictionary() {
                    {"request_type", "create"},
                    {"entity_type", "Task"},
                    {"data", task }
                });
            }
            foreach (PythonDictionary task in updates) {
                int id = (int)task["id"];
                task.Remove("id");
                batch.append(new PythonDictionary() {
                    {"request_type", "update"},
                    {"entity_type", "Task"},
                    {"entity_id", id},
                    {"data", task}
                });
            }
            dynamic connection = connect();
            // record this most-recent activity timestamp on the 'project_plugin' script in Shotgun
            List filter = new List() { new List() {"salted_password", "is", connection.config.api_key} };
            List fields = new List() { "type", "id" };
            PythonDictionary script = connection.find_one("ApiUser", filter, fields: fields);
            String now = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            PythonDictionary timestamp = new PythonDictionary() { {"sg_last_run", now} };
            batch.append(new PythonDictionary() {
                {"request_type", "update"},
                {"entity_type", script["type"]},
                {"entity_id", script["id"]},
                {"data", timestamp}
            });
            connection.batch(batch);
        }
    }
}
