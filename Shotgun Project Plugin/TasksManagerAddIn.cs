/*
 * TODO: Link so open to Shotgun on click
 * TODO: Tooltip on mouse over to show old value
 * TODO: Characters2 support
 */
using System;
using System.Numerics;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MSProject = Microsoft.Office.Interop.MSProject;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;

using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

using IronPython.Runtime;
namespace sg_prj
{
    public partial class TasksManagerAddIn {
        public static Double secondsPerWeek = 4.3;
        public static Double framesPerSecond = 24.0;
        public static Double daysInWorkweek = 5.0;
        public Double framesPerDay = secondsPerWeek * framesPerSecond / daysInWorkweek;
        public String resourceColorField = "Color";
        public String resourceBorderColorField = "Border Color";

        private String[] taskFieldNames = { "Unit", "Animator", "Sequence", "Set", "Set Builds", "Characters" };
        private String[] taskGroupNames = { "Unit", "Animator", "Sequence", "Set", "Set Build", "Characters" };
        private double[] fieldAvailableUnits = { 1, 1, 99999, 99999, 99999, 99999, 99999 };
        private String resourceDeliveryField = "Delivery Dates";
        private String resourceDeliveryValidField = "Delivery Dates Valid";
        private Dictionary<MSProject.PjField, String> taskFieldIds = new Dictionary<MSProject.PjField, String>();
        private Boolean autoCreateResource = false;
        private String missingFields;
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // Python code is lazy loaded, trigger the loading here
            Task.Factory.StartNew(() => Shotgun.Instance);
            // When a new project is activated, see if we should enable ourself
            Microsoft.Office.Interop.MSProject._EProjectApp2_Event appEvents = this.Application as Microsoft.Office.Interop.MSProject._EProjectApp2_Event;
            appEvents.WindowActivate += ThisAddIn_WindowActivate;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        //  Handle a new window being activated.
        //  See if the the new project has the appropriate fields.  If so, listen for changes.
        private void ThisAddIn_WindowActivate(MSProject.Window target)
        {
            // Show the about box if an update has happened
            AboutBox ab = new AboutBox();
            if (Properties.Settings.Default.LastInfoDisplayed.Equals(ab.labelVersion.Text) == false) {
                ab.ShowDialog();
                Properties.Settings.Default.LastInfoDisplayed = ab.labelVersion.Text;
                Properties.Settings.Default.Save();
            }
            // See if all fields are here
            List<String> missing = new List<String>();
            taskFieldIds.Clear();
            foreach (String taskFieldName in taskFieldNames) {
                try {
                    taskFieldIds[this.Application.FieldNameToFieldConstant(taskFieldName)] = taskFieldName;
                } catch {
                    missing.Add(taskFieldName);
                }
            }

            if (missing.Count == 0) {
                this.Application.ProjectBeforeTaskChange += projectBeforeTaskChange;
                missingFields = "";
            } else {
                taskFieldIds.Clear();
                this.Application.ProjectBeforeTaskChange -= projectBeforeTaskChange;
                missingFields = String.Join(", ", missing);
            }
        }

        #region Resources

        private void projectBeforeTaskChange(MSProject.Task tsk, MSProject.PjField field, object newVal, ref bool cancel)
        {
            if (taskFieldIds.ContainsKey(field) == false)
                return; // not interested

            int fieldIndex = Array.IndexOf(taskFieldNames, taskFieldIds[field]);
            String group = taskGroupNames[fieldIndex];
            String[] resNames = newVal.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, Boolean> create = new Dictionary<int, bool>();
            // create resources and find ids
            foreach (String resName in resNames) {
                String name = correctResourceName(resName, group);
                MSProject.Resource res;
                try {
                    res = this.Application.ActiveProject.Resources[name];
                } catch {
                    // Create the resource
                    if (autoCreateResource == false) {
                        DialogResult result = MessageBox.Show(
                            String.Format("Create new resource for {0} '{1}'?", group, name),
                            "Create resource?",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );
                        if (result == DialogResult.No) {
                            return;
                        }
                    }
                    res = this.Application.ActiveProject.Resources.Add(name);
                    res.Group = group;
                    res.Availabilities[1].AvailableUnit = fieldAvailableUnits[fieldIndex];
                }
                create[res.ID] = true;
            }
            // groups can be split across fields, find out resources that are wanted from other fields
            List<String> extraNames = new List<String>();
            for (int i = 0; i < taskFieldNames.Length; ++i) {
                if ((i != fieldIndex) && taskGroupNames[i].Equals(group)) {
                    // another field we should be paying attention to
                    String otherVal = tsk.GetField(this.Application.FieldNameToFieldConstant(taskFieldNames[i]));
                    extraNames.AddRange(otherVal.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            extraNames = (from s in extraNames select s.Trim()).ToList<String>();
            // find out which assignments we need to create, remove unwanted ones
            foreach (MSProject.Assignment asg in tsk.Assignments) {
                if (asg.Resource.Group.Equals(group) == false)
                    continue;
                if (extraNames.Contains(asg.Resource.Name))
                    continue;
                if (create.ContainsKey(asg.Resource.ID))
                    create[asg.Resource.ID] = false;
                else
                    asg.Delete();
            }
            // now create the ones we want
            foreach (int resId in create.Keys) {
                if (create[resId] == false)
                    continue;
                MSProject.Assignment asg = tsk.Assignments.Add(tsk.ID, resId);
                asg.Units = (this.Application.ShowAssignmentUnitsAs == MSProject.PjAssignmentUnits.pjDecimalAssignmentUnits) ? 1.0 : 100;
            }
            // and pretty up the text
            List<String> cleanNames = new List<string>();
            foreach (int resId in create.Keys) {
                cleanNames.Add(this.Application.ActiveProject.Resources[resId].Name);
            }
            cleanNames.Sort();
            String cleanVal = String.Join(", ", cleanNames);
            if (cleanVal.Equals(newVal) == false) {
                // Need to clean up the field value, do it in the background
                Task.Factory.StartNew(() => {
                    int attempts = 0;
                    while (attempts<100) {
                        try {
                            Thread.Sleep(500);
                            tsk.SetField(field, cleanVal);
                            break;
                        } catch (System.Runtime.InteropServices.COMException) {
                            attempts++;
                        }
                    }
                });
            }
        }

        // Take care of common typing mistakes
        private String correctResourceName(String orig, String group)
        {
            // Convert to Title Case
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            String name = textInfo.ToTitleCase(orig.Trim());
            // For Units, let "1" be interpreted as "Unit 1"
            double num;
            if (group.Equals("Unit") && (double.TryParse(name, out num) == true))
                name = "Unit " + name;
            // Trim out double spaces
            name = String.Join(" ", name.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            return name;
        }

        private void fieldsMissingDialog()
        {
            MessageBox.Show(String.Format("Needed fields missing from file: {0}.\nPlease set them up and re-open.", missingFields), "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        internal void PushToResources()
        {
            if (missingFields.Length != 0) {
                fieldsMissingDialog();
                return;
            }
            // setup for no calculations while we're processing the file
            this.Application.ScreenUpdating = false;
            bool oldAutoLevel = this.Application.AutoLevel;
            this.Application.AutoLevel = false;

            // if we have a valid selection use it, otherwise use the whole project
            MSProject.Tasks taskCollection;
            MSProject.Selection sel = this.Application.ActiveSelection;
            if ((sel.Tasks == null) || ((sel.Tasks.Count == 1) && (sel.FieldIDList.Count == 1)))
                taskCollection = this.Application.ActiveProject.Tasks;
            else
                taskCollection = this.Application.ActiveSelection.Tasks;

            // setup the progress bar
            Progress pBar = new Progress();
            pBar.progressBar.Minimum = 1;
            pBar.progressBar.Maximum = taskCollection.Count;
            pBar.progressBar.Step = 1;
            pBar.progressBar.Value = 1;
            pBar.label.Text = String.Format("{0}/{1}", 1, taskCollection.Count);
            pBar.Show();
            bool canceled = false;
            pBar.cancel.Click += new EventHandler((sender, e) => {
                canceled = true;
                pBar.label.Text = "Cancelling";
            });

            // grab the fieldIDs for our custom fields
            List<MSProject.PjField> fieldIDs = new List<MSProject.PjField>();
            foreach (String fieldName in this.taskFieldNames) {
                fieldIDs.Add(this.Application.FieldNameToFieldConstant(fieldName));
            }

            // go through each task and update resources
            foreach (MSProject.Task tsk in taskCollection) {
                // loop through the fields we care about
                for (int i = 0; i < fieldIDs.Count; ++i) {
                    bool cancel = false;
                    this.projectBeforeTaskChange(tsk, fieldIDs[i], tsk.GetField(fieldIDs[i]), ref cancel);
                    System.Windows.Forms.Application.DoEvents();
                } // for field
                if (canceled)
                    break;
                pBar.progressBar.PerformStep();
                pBar.label.Text = String.Format("{0}/{1}", pBar.progressBar.Value, taskCollection.Count);
                pBar.Update();
            } // foreach tsk

            // reset original state
            this.Application.AutoLevel = oldAutoLevel;
            this.Application.ScreenUpdating = true;
            pBar.Dispose();
        }

        #endregion // resources
        #region Availability

        internal void UpdateDeliveries()
        {
            if (missingFields.Length != 0) {
                fieldsMissingDialog();
                return;
            }
            this.Application.OpenUndoTransaction("Update Deliveries");

            // Find fieldID for the delivery column
            MSProject.PjField deliveryID = this.Application.FieldNameToFieldConstant(this.resourceDeliveryField, MSProject.PjFieldType.pjResource);
            MSProject.PjField deliveryValidID = this.Application.FieldNameToFieldConstant(this.resourceDeliveryValidField, MSProject.PjFieldType.pjResource);

            // if we have a valid selection use it, otherwise use the whole project
            MSProject.Resources resourceCollection;
            if (this.Application.ActiveSelection.Resources.Count > 1 || this.Application.ActiveSelection.FieldIDList.Count > 1)
                resourceCollection = this.Application.ActiveSelection.Resources;
            else {
                resourceCollection = this.Application.ActiveProject.Resources;
            }

            // go through each resource and update availability
            foreach (MSProject.Resource res in resourceCollection) {
                String[] deliveries = res.GetField(deliveryID).Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (deliveries.Count() == 0)
                    continue;
                SortedDictionary<DateTime, int> availabilities = new SortedDictionary<DateTime, int>();
                foreach (String delivery in deliveries) {
                    Boolean isValid = true;
                    // split on '*', valid format is <date>[*count] with <date> as a valid date string or 'W<week number>' and count an integer
                    String[] parts = delivery.Split(new Char[] { '*' });
                    if (parts.Count() > 2)
                        isValid = false;
                    // figure out count
                    int count = 1;
                    if (isValid) {
                        if (parts.Count() == 2) {
                            if (int.TryParse(parts[1], out count) == false)
                                isValid = false;
                        }
                    }
                    // figure out date
                    String dateStr = parts[0];
                    if (isValid) {
                        if (dateStr.ToUpper()[0] == 'W') {
                            double rawWeekNumber;
                            if (double.TryParse(dateStr.Substring(1, dateStr.Length - 1), out rawWeekNumber) == true) {
                                // valid week specifier, figure date since start
                                int dayOfWeek = (int)Math.Round(10.0 * (rawWeekNumber - Math.Truncate(rawWeekNumber)));
                                int weekNumber = (int)rawWeekNumber;
                                if ((dayOfWeek > 7) || (weekNumber < 1))
                                    isValid = false;
                                if (isValid) {
                                    // convert for easy math, weekNumber's start on 1, for DayOfWeek, both 0 and 1 refer to
                                    // the first day of the week
                                    weekNumber -= 1;
                                    dayOfWeek -= ((dayOfWeek == 0) ? 0 : 1);
                                    // and come up with our answer
                                    dateStr = this.Application.ActiveProject.ProjectSummaryTask.Start.AddDays((7 * weekNumber) + dayOfWeek).ToString();
                                }
                            }
                        }
                    }
                    // update availabilities
                    if (isValid) {
                        try {
                            availabilities[DateTime.Parse(dateStr)] = count;
                        } catch {
                            isValid = false;
                        }
                    }
                    // update formatting for invalid strings
                    res.SetField(deliveryValidID, isValid ? "Yes" : "No");
                } // foreach delivery
                // clear current availabilities from the end
                for (int i = res.Availabilities.Count; i > 0; --i) {
                    res.Availabilities[i].AvailableTo = "NA";
                    res.Availabilities[i].AvailableFrom = "NA";
                    res.Availabilities[i].AvailableUnit = 0;
                }
                // add in the new ones
                int currentAvailability = 1;
                foreach (KeyValuePair<DateTime, int> kvp in availabilities) {
                    if (currentAvailability > 1) {
                        res.Availabilities[currentAvailability - 1].AvailableTo = kvp.Key.AddDays(-1).ToShortDateString();
                    }
                    if (currentAvailability <= res.Availabilities.Count) {
                        res.Availabilities[currentAvailability].AvailableFrom = kvp.Key.ToShortDateString();
                        res.Availabilities[currentAvailability].AvailableTo = "NA";
                        res.Availabilities[currentAvailability].AvailableUnit = kvp.Value;
                    } else {
                        res.Availabilities.Add(kvp.Key.ToShortDateString(), "NA", kvp.Value);
                    }
                    currentAvailability += 1;
                }
            } // foreach res

            this.Application.CloseUndoTransaction();
        }

        #endregion // availability
        #region Coloring

        internal void ColorByResource(String group)
        {
            if (missingFields.Length != 0) {
                fieldsMissingDialog();
                return;
            }
            this.Application.OpenUndoTransaction("Color By " + group);

            MSProject.PjField resourceColorField = this.Application.FieldNameToFieldConstant(this.resourceColorField, MSProject.PjFieldType.pjResource);
            this.Application.ScreenUpdating = false;
            String origView = this.Application.ActiveProject.CurrentView;
            // clear team planner first - TODO: CLEAN THIS UP
            this.Application.ViewApply("Team Planner");
            this.Application.FilterClear();
            // update gantt chart
            this.Application.ViewApply("&Gantt Chart");
            String origFilter = this.Application.ActiveProject.CurrentFilter;
            String origGroup = this.Application.ActiveProject.CurrentGroup;
            this.Application.GroupClear();
            foreach (MSProject.Resource res in this.Application.ActiveProject.Resources) {
                if (res.Group != group)
                    continue;
                this.Application.ViewApply("&Gantt Chart");
                this.Application.FilterApply("Using Resource...", Value1: res.Name);
                this.Application.SelectBeginning();
                this.Application.SelectEnd(true);
                System.Drawing.Color color = this.stringToColor(res.GetField(resourceColorField), System.Drawing.Color.White);
                this.Application.GanttBarFormatEx(MiddleColor: color);
                this.Application.FilterClear();
                // update team planner
                this.Application.ViewApply("Resource Usage");
                this.Application.SetAutoFilter(
                    this.Application.FieldConstantToFieldName(MSProject.PjField.pjResourceName),
                    MSProject.PjAutoFilterType.pjAutoFilterCustom, "equals", res.Name);
                this.Application.OutlineHideSubTasks();
                this.Application.OutlineShowAllTasks();
                int row = 1;
                this.Application.ScreenUpdating = true;
                this.Application.SelectRow(row++);
                while (this.Application.ActiveSelection.Resources != null) {
                    this.Application.SelectTaskAssns();
                    this.Application.ViewApply("Team Planner");
                    this.Application.SegmentFillColor(this.colorToInt(color));
                    this.Application.ViewApply("Resource Usage");
                    this.Application.SelectRow(row++);
                }
                this.Application.FilterClear();
            }
            try { this.Application.FilterApply(origFilter); } catch { }
            try { this.Application.GroupApply(origGroup); } catch { }
            // back to original view
            try { this.Application.ViewApply(origView); } catch { }
            this.Application.ScreenUpdating = true;

            this.Application.CloseUndoTransaction();
        }

        internal int colorToInt(System.Drawing.Color color)
        {
            return ((0xFF0000 & (color.B << 32)) |
                    (0x00FF00 & (color.G << 16)) |
                    (0x0000FF & (color.R)));
        }

        internal String colorToString(System.Drawing.Color color)
        {
            throw new NotImplementedException();
        }

        internal System.Drawing.Color stringToColor(String colorStr, System.Drawing.Color defaultColor)
        {
            String[] hexValues = colorStr.Split(' ');
            if (hexValues.Count() != 3)
                return defaultColor;
            try {
                return System.Drawing.Color.FromArgb(
                    Convert.ToByte(hexValues[0], 16),
                    Convert.ToByte(hexValues[1], 16),
                    Convert.ToByte(hexValues[2], 16)
                );
            } catch {
                return defaultColor;
            }
        }

        #endregion // Coloring
        #region Shotgun Push/Pull

        private void _PopulateGridWithTasks(PushToShotgunForm form)
        {
            // field ids
            MSProject.PjField setsFieldId = this.Application.FieldNameToFieldConstant("Set");
            MSProject.PjField setBuildsFieldId = this.Application.FieldNameToFieldConstant("Set Builds");
            MSProject.PjField unitFieldId = this.Application.FieldNameToFieldConstant("Unit");
            MSProject.PjField shotFieldId = this.Application.FieldNameToFieldConstant("Shot");
            MSProject.PjField charactersFieldId = this.Application.FieldNameToFieldConstant("Characters");
            // populate grid
            DataGridViewRowCollection rows = form.taskGrid.Rows;
            rows.Clear();
            DateTime endDate = form.toDate.MinDate;
            DateTime startDate = form.fromDate.MaxDate;
            foreach (MSProject.Task task in this.Application.ActiveProject.Tasks) {
                if (task.Active == false)
                    continue;
                // unit fixes
                String unit = task.GetField(unitFieldId);
                // set fixes
                List<String> setNames = task.GetField(setsFieldId).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                setNames.Sort();
                String sets = String.Join(", ", from s in setNames select s.Trim());
                // set build fixes
                List<String> setBuildNames = task.GetField(setBuildsFieldId).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                setBuildNames.Sort();
                String setBuilds = String.Join(", ", from s in setBuildNames select s.Trim());
                // character fixes
                List<String> charNames = task.GetField(charactersFieldId).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                charNames.Sort();
                String chars = String.Join(",", from c in charNames select c.Trim());

                // add the row
                int row = rows.Add();
                rows[row].Cells[form.taskGrid.Columns["SyncColumn"].Index].Value = true;
                rows[row].Cells[form.taskGrid.Columns["StatusColumn"].Index].Value = "";
                rows[row].Cells[form.taskGrid.Columns["TaskColumn"].Index].Value = task.Name;
                rows[row].Cells[form.taskGrid.Columns["StartColumn"].Index].Value = task.Start;
                rows[row].Cells[form.taskGrid.Columns["EndColumn"].Index].Value = task.Finish;
                rows[row].Cells[form.taskGrid.Columns["DurationColumn"].Index].Value = task.Duration;
                rows[row].Cells[form.taskGrid.Columns["UnitColumn"].Index].Value = unit;
                rows[row].Cells[form.taskGrid.Columns["SetsColumn"].Index].Value = sets;
                rows[row].Cells[form.taskGrid.Columns["SetBuildsColumn"].Index].Value = setBuilds;
                rows[row].Cells[form.taskGrid.Columns["ShotColumn"].Index].Value = task.GetField(shotFieldId);
                rows[row].Cells[form.taskGrid.Columns["CharactersColumn"].Index].Value = chars;
                form.initRow(rows[row], projectTask: task);
                if (task.Start < startDate)
                    startDate = task.Start;
                if (task.Finish > endDate)
                    endDate = task.Finish;
            }
            form.toDate.Value = endDate;
            form.fromDate.Value = startDate;
        }

        private void _CollectShotgunData(Action<bool> postProcess, Dictionary<string, PythonDictionary> results, ToolStripItem statusItem = null, Button cancelButton = null, bool pushToShotgun = false)
        {
            bool canceled = false;
            DateTime workerStart = DateTime.Now;

            // Setup background workers
            Dictionary<String, Func<BackgroundWorker, DoWorkEventArgs, PythonDictionary>> workFuncs;
            if (pushToShotgun)
                workFuncs = new Dictionary<String, Func<BackgroundWorker, DoWorkEventArgs, PythonDictionary>>() {
                    {"tasks", Shotgun.Instance.getTasks},
                    {"units", Shotgun.Instance.getUnits},
                    {"sets", Shotgun.Instance.getSets},
                    {"setBuilds", Shotgun.Instance.getSetBuilds},
                    {"characters", Shotgun.Instance.getPuppets},
                    {"shots", Shotgun.Instance.getShots},
                    {"_pipeline step", Shotgun.Instance.getStageStep},
                };
            else
                workFuncs = new Dictionary<String, Func<BackgroundWorker, DoWorkEventArgs, PythonDictionary>>() {
                    {"tasks", Shotgun.Instance.getTasks},
                };
            Dictionary<String, int> counts = workFuncs.Keys.ToDictionary(key => key, key => 0);
            Dictionary<String, bool> dones = workFuncs.Keys.ToDictionary(key => key, key => false);
            Dictionary<String, BackgroundWorker> workers = new Dictionary<String, BackgroundWorker>();
            foreach (String keyName in workFuncs.Keys) {
                String name = keyName;
                BackgroundWorker worker = new BackgroundWorker();
                if (statusItem != null)
                    worker.WorkerReportsProgress = true;
                if (cancelButton != null)
                    worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler((bw, e) => {
                    results[name] = workFuncs[name](bw as BackgroundWorker, e);
                    TaskRow.shotgunData[name] = results[name];
                });
                if (statusItem != null) {
                    worker.ProgressChanged += new ProgressChangedEventHandler((bw, e) => {
                        // keep track of how many we've gotten so far
                        counts[name] = (int)e.UserState;
                        // update status string
                        lock (statusItem) {
                            String status = String.Format("[{0}s] Downloading shotgun data: ", (int)(DateTime.Now - workerStart).TotalSeconds);
                            foreach (String countName in counts.Keys)
                                if (countName.StartsWith("_") == false)
                                    status += String.Format(" {0} {1}.", counts[countName], countName);
                            statusItem.Text = status;
                        }
                    });
                }
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((bw, e) => {
                    // if everybody is done then run the post processing step
                    lock (dones) {
                        dones[name] = true;
                        if (dones.Values.Contains(false) == false)
                            postProcess(canceled);
                    }
                });
                // actually add the new worker to the dictionary
                workers[name] = worker;
            }
            // link up cancel button
            if (cancelButton != null) {
                cancelButton.Click += delegate(Object sender, EventArgs e) {
                    canceled = true;
                    foreach (BackgroundWorker worker in workers.Values)
                        worker.CancelAsync();
                };
            }
            // and run 'em
            foreach (BackgroundWorker worker in workers.Values)
                worker.RunWorkerAsync();
        }

        private void _PopulateGridWithShotgunData(PushToShotgunForm form, Dictionary<string, PythonDictionary> results, Boolean pushToShotgun)
        {
            Hashtable foundTasks = new Hashtable();
            foreach (String key in results["tasks"].Keys) {
                PythonDictionary sgTask = (PythonDictionary)results["tasks"][key];
                foundTasks[key] = false;
            }
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in form.taskGrid.Rows)
                rows.Add(row);
            form.taskGrid.Rows.Clear();
            int taskColumn = form.taskGrid.Columns["TaskColumn"].Index;
            int shotColumn = form.taskGrid.Columns["ShotColumn"].Index;
            for (int i = 0; i < rows.Count; ++i) {
                DataGridViewRow row = rows[i];
                string rowKey = row.Cells[taskColumn].Value + " --++-- " + row.Cells[shotColumn].Value;
                foundTasks[rowKey] = true;
                if (results["tasks"].ContainsKey(rowKey) == true)
                    (row.Tag as TaskRow).shotgunTask = (PythonDictionary)results["tasks"][rowKey];
            }

            // add extra shotgun tasks
            DateTime fromDate = form.fromDate.Value;
            DateTime toDate = form.toDate.Value;
            foreach (String taskKey in results["tasks"].Keys) {
                if ((bool)foundTasks[taskKey] == true)
                    continue;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(form.taskGrid);
                PythonDictionary shotgunTask = results["tasks"][taskKey] as PythonDictionary;
                if ((shotgunTask["start_date"] != null) && ((DateTime)shotgunTask["start_date"] < fromDate))
                    fromDate = (DateTime)shotgunTask["start_date"];
                if ((shotgunTask["due_date"] != null) && ((DateTime)shotgunTask["due_date"] > toDate))
                    toDate = (DateTime)shotgunTask["due_date"];
                form.initRow(newRow, shotgunTask: shotgunTask);
                newRow.Cells[form.taskGrid.Columns["SyncColumn"].Index].Value = (pushToShotgun == false);
                rows.Add(newRow);
            }
            form.toDate.Value = toDate;
            form.fromDate.Value = fromDate;

            // add the rows back in
            form.taskGrid.Rows.AddRange(rows.ToArray());
        }

        internal void PushToShotgun()
        {
            if (missingFields.Length != 0) {
                fieldsMissingDialog();
                return;
            }
            // Setup form
            PushToShotgunForm form = new PushToShotgunForm();
            form.Text = String.Format("Push To Shotgun - {0}", Properties.Settings.Default.ShotgunProjectName);
            form.Ok.Text = "Push";
            form.syncLabel.Text = "Fields to Push:";
            // sync defaults
            form.syncDates.Checked = true;
            form.syncUnits.Checked = true;
            TaskRow.mode = TaskRow.MODES.PushToShotgun;
            form.Cursor = Cursors.WaitCursor;
            _PopulateGridWithTasks(form);

            // this will run once we grab all the info from shotgun
            Dictionary<String, PythonDictionary> results = new Dictionary<String, PythonDictionary>();
            Action<bool> post = new Action<bool>((canceled) => {
                if (canceled)
                    return;
                form.Text = String.Format("Push To Shotgun - {0}", Properties.Settings.Default.ShotgunProjectName);
                _PopulateGridWithShotgunData(form, results, pushToShotgun: true);
                form.updateRows();
                form.taskGrid.Sort(form.taskGrid.Columns["TaskColumn"], ListSortDirection.Ascending);
                form.Ok.Enabled = true;
                form.Cursor = Cursors.Default;
            });

            // Collect shotgun data
            _CollectShotgunData(post, results, form.status.Items[0], form.Cancel, pushToShotgun: true);

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            form.Show();

            // send it on to Shotgun
            form.Cursor = Cursors.WaitCursor;
            form.status.Items[0].Text = "Uploading to shotgun.";
            System.Windows.Forms.Application.DoEvents();
            List creates = new List();
            List updates = new List();
            foreach (DataGridViewRow row in form.taskGrid.Rows) {
                if ((bool)row.Cells["SyncColumn"].Value == false)
                    continue;

                PythonDictionary sgTask = (row.Tag as TaskRow).shotgunTask;
                MSProject.Task projectTask = (row.Tag as TaskRow).projectTask;
                switch ((String)row.Cells["StatusColumn"].Value) {
                    case "Skip":
                    case "Error":
                        continue;
                    case "Omit":
                    {
                        updates.append(new PythonDictionary() {
                            {"id", sgTask["id"]},
                            {"sg_status_list", "omt"},
                        });
                        break;
                    }
                    case "Create":
                    case "Update":
                    {
                        PythonDictionary data;
                        data = new PythonDictionary() {
                            {"sg_status_list", "wtg"},
                            {"sg_task_type", "YourProjectSyncableTaskType"},
                            {"sg_description", projectTask.Notes},
                            {"content", row.Cells["TaskColumn"].Value},
                            {"step", results["_pipeline step"]},
                            {"entity", results["shots"][row.Cells["ShotColumn"].Value]},
                            {"sg_task_mode", projectTask.Manual ? "Manual" : "Auto"},
                        };
                        if (form.syncDates.Checked) {
                            data["start_date"] = ((DateTime)row.Cells["StartColumn"].Value).ToString("yyyy-MM-dd");
                            data["due_date"] = ((DateTime)row.Cells["EndColumn"].Value).ToString("yyyy-MM-dd");
                        }
                        if (form.syncDurations.Checked)
                        {
                            data["duration"] = BigInteger.Parse(row.Cells["DurationColumn"].Value.ToString());
                        }
                        if (form.syncSets.Checked) {
                            List sgTaskSets = new List();
                            foreach (String setName in ((String)row.Cells["SetsColumn"].Value).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                sgTaskSets.append(results["sets"][setName.Trim()]);
                            data["sg_set_builds"] = sgTaskSets;
                        }
                        if (form.syncSetBuilds.Checked)
                        {
                            List sgTaskSetBuilds = new List();
                            foreach (String setBuildName in ((String)row.Cells["SetBuildsColumn"].Value).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                sgTaskSetBuilds.append(results["setBuilds"][setBuildName.Trim()]);
                            data["sg_set_phases"] = sgTaskSetBuilds;
                        }
                        if (form.syncUnits.Checked)
                            if ((row.Cells["UnitColumn"].Value as String) == "")
                                data["sg_unit"] = IronPython.Modules.Builtin.None;
                            else
                                data["sg_unit"] = results["units"][row.Cells["UnitColumn"].Value];
                        if (form.syncCharacters.Checked) {
                            List sgTaskCharacters = new List();
                            foreach (String characterName in ((String)row.Cells["CharactersColumn"].Value).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                sgTaskCharacters.append(results["characters"][characterName.Trim()]);
                            data["sg_puppets"] = sgTaskCharacters;
                        }
                        if ((String)row.Cells["StatusColumn"].Value == "Update") {
                            data["id"] = sgTask["id"];
                            updates.append(data);
                        } else {
                            creates.append(data);
                        }
                        break;
                    }
                } // switch status
            } // foreach row
            Shotgun.Instance.updateTasks(creates, updates);
            form.Dispose();
            MessageBox.Show("Tasks updated.");
        }

        internal int getTotalActionRowCount(PushToShotgunForm form)
        {
            Dictionary<String, int> statusCounts = form.getStatusCounts();
            return statusCounts["Omit"] + statusCounts["Remove"] + statusCounts["Create"] + statusCounts["Update"];
        }

        internal String getSceneNameFromTaskName(DataGridViewRow row)
        {   
            String result = (string)row.Cells["TaskColumn"].Value;
            String [] parentSceneNameParts = result.Split('/');
            if (parentSceneNameParts.Length > 0)
            {
                result = parentSceneNameParts[0];
            }
            return result;
        }

        internal void PullFromShotgun()
        {
            if (missingFields.Length != 0) {
                fieldsMissingDialog();
                return;
            }
            // Setup form
            PushToShotgunForm form = new PushToShotgunForm();
            form.Text = String.Format("Pull From Shotgun - {0}", Properties.Settings.Default.ShotgunProjectName);
            form.Ok.Text = "Pull";
            form.syncLabel.Text = "Fields to Pull:";
            // sync defaults
            form.syncSets.Checked = true;
            form.syncSetBuilds.Checked = true;
            form.syncCharacters.Checked = true;
            TaskRow.mode = TaskRow.MODES.PullFromShotgun;
            form.Cursor = Cursors.WaitCursor;
            _PopulateGridWithTasks(form);

            // this will run once we grab all the info from shotgun
            Dictionary<String, PythonDictionary> results = new Dictionary<String, PythonDictionary>();
            Action<bool> post = new Action<bool>((canceled) => {
                if (canceled)
                    return;
                form.Text = String.Format("Pull From Shotgun - {0}", Properties.Settings.Default.ShotgunProjectName);
                _PopulateGridWithShotgunData(form, results, pushToShotgun: false);
                form.updateRows();
                form.taskGrid.Sort(form.taskGrid.Columns["TaskColumn"], ListSortDirection.Ascending);
                form.Ok.Enabled = true;
                form.Cursor = Cursors.Default;
            });

            // Collect shotgun data
            _CollectShotgunData(post, results, form.status.Items[0], form.Cancel, pushToShotgun: false);

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            // process results
            autoCreateResource = true;
            form.Cursor = Cursors.Default;
            this.Application.OpenUndoTransaction("Pull from Shotgun");
            MSProject.PjCalculation oldCalcValue = this.Application.Calculation;
            this.Application.Calculation = MSProject.PjCalculation.pjManual;
            this.Application.ScreenUpdating = false;
            MSProject.PjField setsFieldId = this.Application.FieldNameToFieldConstant("Set");
            MSProject.PjField setBuildsFieldId = this.Application.FieldNameToFieldConstant("Set Builds");
            MSProject.PjField unitFieldId = this.Application.FieldNameToFieldConstant("Unit");
            MSProject.PjField shotFieldId = this.Application.FieldNameToFieldConstant("Shot");
            MSProject.PjField charactersFieldId = this.Application.FieldNameToFieldConstant("Characters");
            MSProject.PjField priorityFieldId = this.Application.FieldNameToFieldConstant("Priority");
            MSProject.PjField animatorFieldId = this.Application.FieldNameToFieldConstant("Animator");

            // setup the progress bar
            int totalActionRowCount = getTotalActionRowCount(form); 
            Progress pBar = new Progress();
            pBar.progressBar.Minimum = 1;
            pBar.progressBar.Maximum = totalActionRowCount;
            pBar.progressBar.Step = 1;
            pBar.progressBar.Value = 1;
            
            pBar.label.Text = String.Format("{0}/{1}", 1, totalActionRowCount);
            pBar.Show();
            bool stop = false;
            pBar.cancel.Click += new EventHandler((sender, e) => {
                stop = true;
                pBar.label.Text = "Cancelling";
            });

            // Build dictionary of scene tasks.  Keyed by scene name
            Dictionary<string, MSProject.Task> sceneNameToProjectTask = new Dictionary<string,MSProject.Task>();
            foreach (DataGridViewRow row in form.taskGrid.Rows){
                MSProject.Task projectTask = (row.Tag as TaskRow).projectTask;
                if (projectTask == null || !projectTask.GetField(shotFieldId).Contains(".scn"))
                    continue;
                String parentSceneName = getSceneNameFromTaskName(row);
                if (!sceneNameToProjectTask.ContainsKey(parentSceneName)) {
                    sceneNameToProjectTask.Add(parentSceneName, projectTask);
                }
            }

            foreach (DataGridViewRow row in form.taskGrid.Rows) {
                if (stop)
                    break;
                if ((bool)row.Cells["SyncColumn"].Value == false)
                    continue;

                switch ((String)row.Cells["StatusColumn"].Value) {
                    case "Skip":
                        continue;
                    case "Remove":
                    {
                        MSProject.Task pjTask = (row.Tag as TaskRow).projectTask;
                        pjTask.Delete();
                        String parentSceneName = getSceneNameFromTaskName(row);
                        if (sceneNameToProjectTask.ContainsKey(parentSceneName))
                        {
                            sceneNameToProjectTask.Remove(parentSceneName);
                        }
                        break;
                    }
                    case "Update":
                    case "Create":
                    {
                        String unit = (String)row.Cells["UnitColumn"].Value;

                        PythonDictionary sgTask = (row.Tag as TaskRow).shotgunTask;
                        MSProject.Task pjTask = (row.Tag as TaskRow).projectTask;
                        if (pjTask == null) {
                            pjTask = this.Application.ActiveProject.Tasks.Add(row.Cells["TaskColumn"].Value);
                            // Basic values
                            pjTask.LevelingCanSplit = false;
                            String shotName = (String)row.Cells["ShotColumn"].Value;
                            pjTask.SetField(shotFieldId, shotName);
                            String sequence;
                            sequence = (sgTask["entity.Shot.sg_sequence"] as PythonDictionary)["name"] as String;
                            pjTask.SetField(pjTask.Application.FieldNameToFieldConstant("Sequence"), sequence);
                            List parents = (sgTask["entity.Shot.parent_shots"] as List);
                            if (parents.Count > 0) {
                                String sceneName = (parents[0] as PythonDictionary)["name"] as String;
                                pjTask.SetField(pjTask.Application.FieldNameToFieldConstant("Scene"), sceneName);
                                if (sceneNameToProjectTask.ContainsKey(sceneName) ) {
                                    // Look for a task for the parent scene already in the project file
                                    // If found, we can copy down the Unit, Priority and Animator values to
                                    // the child shot.  This will make the shots show up in approximately
                                    // the same place as the parent scene.
                                    MSProject.Task parentSceneTask = sceneNameToProjectTask[sceneName];
                                    pjTask.SetField(unitFieldId, parentSceneTask.GetField(unitFieldId));
                                    pjTask.SetField(priorityFieldId, parentSceneTask.GetField(priorityFieldId));
                                    pjTask.SetField(animatorFieldId, parentSceneTask.GetField(animatorFieldId));
                                }
                            }
                        }
                        // Duration
                        if (!pjTask.Manual)
                        {
                            if (sgTask["duration"] != null)
                                pjTask.Duration = sgTask["duration"];
                            else if (sgTask["entity.Shot.sg_cut_duration"] != null)
                            {
                                // use quota
                                int days = (int)(Math.Ceiling(((int)(sgTask["entity.Shot.sg_cut_duration"])) / framesPerDay));
                                pjTask.Duration = String.Format("{0}d", days);
                            }
                        }

                        pjTask.Notes = (sgTask["sg_description"] == null) ? "" : (sgTask["sg_description"] as String);
                        if (form.syncDates.Checked) {
                            if ((row.Cells["StartColumn"].Value != null) && (row.Cells["EndColumn"].Value != null)) {
                                pjTask.Manual = true;
                                pjTask.Start = "";
                                pjTask.Finish = "";
                                pjTask.Duration = "";
                                pjTask.Start = row.Cells["StartColumn"].Value;
                                pjTask.Finish = row.Cells["EndColumn"].Value;
                            }
                        }
                        if (form.syncCharacters.Checked)
                        {
                            // Make sure that the total length on the character field doesn't blow the MS-Project
                            // limit of 255 chars
                            String charactersString = (String)row.Cells["CharactersColumn"].Value;
                            if (charactersString.Length > 255)
                            {
                                charactersString = "";
                                foreach (String characterName in ((String)row.Cells["CharactersColumn"].Value).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (charactersString.Length + characterName.Length + 5 >= 255)
                                    {
                                        charactersString += "," + TaskRow.overflowMarker;
                                        break;
                                    }
                                    charactersString += characterName;
                                    charactersString += ",";
                                }
                            }
                            pjTask.SetField(charactersFieldId, charactersString);
                        }
                        if (form.syncSets.Checked)
                            pjTask.SetField(setsFieldId, (String)row.Cells["SetsColumn"].Value);
                        if (form.syncSetBuilds.Checked)
                            pjTask.SetField(setBuildsFieldId, (String)row.Cells["SetBuildsColumn"].Value);
                        if (form.syncUnits.Checked)
                            pjTask.SetField(unitFieldId, unit);

                        break;
                    }
                    default:
                        throw new Exception("Unknown status: " + (String)row.Cells["StatusColumn"].Value);
                }

                pBar.progressBar.PerformStep();
                pBar.label.Text = String.Format("{0}/{1}", pBar.progressBar.Value, totalActionRowCount);
                pBar.Update();
            }
            autoCreateResource = false;
            this.Application.Calculation = oldCalcValue;
            this.Application.ScreenUpdating = true;
            this.Application.CloseUndoTransaction();
            pBar.Dispose();
            MessageBox.Show("Project tasks updated.");
            form.Dispose();
        }

        internal void SetShotgunProject()
        {
            Shotgun.Instance.setDefaultProject();
        }
        #endregion // Shotgun Push/Pull
    }
}