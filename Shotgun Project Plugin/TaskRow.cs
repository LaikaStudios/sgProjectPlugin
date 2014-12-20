using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Runtime;
using MSProject = Microsoft.Office.Interop.MSProject;

namespace sg_prj {
    class TaskRow {
        public enum MODES {
            PushToShotgun,
            PullFromShotgun,
        }

        [Flags]
        public enum SYNC_FIELDS {
            Dates = 0x01,
            Units = 0x02,
            Sets = 0x04,
            Characters = 0x08,
            SetBuilds = 0x10,
            Durations = 0x20
        }

        public static Dictionary<String, PythonDictionary> shotgunData = new Dictionary<string, PythonDictionary>();
        public static MODES mode;
        public static SYNC_FIELDS sync;
        public const String overflowMarker = "XTRA";

        public PythonDictionary shotgunTask;
        public MSProject.Task projectTask;

        // constructor
        public TaskRow(PythonDictionary shotgunTaskIn = null, MSProject.Task projectTaskIn = null)
        {
            shotgunTask = shotgunTaskIn;
            projectTask = projectTaskIn;
        }

        // indexer
        public bool has_key(String index)
        {
            switch (index) {
                case "Task":
                case "Start":
                case "End":
                case "Duration":
                case "Unit":
                case "Shot":
                case "Sets":
                case "Set Builds":
                case "Characters":
                    return true;
                default:
                    return false;
            }
        }

        public Hashtable this[String index]
        {
            get
            {
                switch (index) {
                    case "Task":
                        return this.task;
                    case "Start":
                        return this.start;
                    case "End":
                        return this.end;
                    case "Duration":
                        return this.duration;
                    case "Unit":
                        return this.unit;
                    case "Shot":
                        return this.shot;
                    case "Sets":
                        return this.sets;
                    case "Characters":
                        return this.characters;
                    case "Set Builds":
                        return this.setBuilds;

                    default:
                        throw new Exception("unknown column: " + index);
                }
            }
        }

        // properties
        public string status
        {
            get
            {
                // Error - never on pull, when setting an item to a non-existant entity on push
                if (mode == MODES.PushToShotgun) {

                    // Omit - never on pull, when sgTask exists but projectTask does not on push
                    if ((shotgunTask != null) && (projectTask == null))
                        return "Omit";

                    if ((bool)this.shot["errored"] == true)
                        return "Error";
                    if ((sync & SYNC_FIELDS.Characters) == SYNC_FIELDS.Characters) {
                        if ((bool)this.characters["errored"] == true)
                            return "Error";
                    }
                    if ((sync & SYNC_FIELDS.Sets) == SYNC_FIELDS.Sets) {
                        if ((bool)this.sets["errored"] == true)
                            return "Error";
                    }
                    if ((sync & SYNC_FIELDS.Units) == SYNC_FIELDS.Units) {
                        if ((bool)this.unit["errored"] == true)
                            return "Error";
                    }
                }
                // Remove - never on push, when projectTask exists but shotgunTask does not on pull
                if (mode == MODES.PullFromShotgun) {
                    if ((projectTask != null) && (shotgunTask == null))
                        return "Remove";
                }
                // Create
                if ((mode == MODES.PullFromShotgun) && (shotgunTask !=null) && (projectTask == null))
                    return "Create";
                if ((mode == MODES.PushToShotgun) && (projectTask != null) && (shotgunTask == null))
                    return "Create";
                // Update
                if ((sync & SYNC_FIELDS.Dates) == SYNC_FIELDS.Dates) {
                    if (((bool)this.start["different"] == true) || ((bool)this.end["different"] == true))
                        return "Update";
                }
                if ((sync & SYNC_FIELDS.Characters) == SYNC_FIELDS.Characters) {
                    if ((bool)this.characters["different"] == true)
                        return "Update";
                }
                if ((sync & SYNC_FIELDS.Sets) == SYNC_FIELDS.Sets) {
                    if ((bool)this.sets["different"] == true)
                        return "Update";
                }
                if ((sync & SYNC_FIELDS.Units) == SYNC_FIELDS.Units) {
                    if ((bool)this.unit["different"] == true)
                        return "Update";
                }

                if (mode == MODES.PushToShotgun && (projectTask.Manual && ((string)shotgunTask["sg_task_mode"]) != "Manual"))
                {
                    // Pushing tasks and task has been manually scheduled, need to update shotgun
                    return "Update";
                }
                if (mode == MODES.PullFromShotgun && (!projectTask.Manual && (bool)this.duration["different"]))
                {
                    // Pulling tasks, task is not manual and durations are different
                    return "Update";
                }

                // All the same, Skip
                return "Skip";
            }
        } // status property

        public Hashtable task
        {
            get
            {
                Hashtable _task = new Hashtable();
                if (mode == MODES.PullFromShotgun)
                    _task["value"] = (shotgunTask == null) ? projectTask.Name : shotgunTask["content"];
                if (mode == MODES.PushToShotgun)
                    _task["value"] = (projectTask == null) ? shotgunTask["content"] : projectTask.Name;
                _task["errored"] = false;
                _task["different"] = false;
                return _task;
            }
        }

        public Hashtable start
        {
            get
            {
                Hashtable _start = new Hashtable();
                if (mode == MODES.PullFromShotgun)
                    _start["value"] = (shotgunTask == null) ? projectTask.Start : shotgunTask["start_date"];
                if (mode == MODES.PushToShotgun)
                    _start["value"] = (projectTask == null) ? shotgunTask["start_date"] : projectTask.Start;
                _start["errored"] = false;
                _start["different"] = (shotgunTask == null) ||
                    (projectTask == null) ||
                    ((shotgunTask["start_date"] == null) && (projectTask.Start != null)) ||
                    ((shotgunTask["start_date"] != null) && (projectTask.Start == null)) ||
                    ((shotgunTask["start_date"] != null) && (projectTask.Start != null) && ((DateTime)shotgunTask["start_date"] != (DateTime)(projectTask.Start)));
                return _start;
            }
        }

        public Hashtable end
        {
            get
            {
                Hashtable _end = new Hashtable();
                if (mode == MODES.PullFromShotgun)
                    _end["value"] = (shotgunTask == null) ? projectTask.Finish : shotgunTask["due_date"];
                if (mode == MODES.PushToShotgun)
                    _end["value"] = (projectTask == null) ? shotgunTask["due_date"] : projectTask.Finish;
                _end["errored"] = false;
                _end["different"] = (shotgunTask == null) ||
                    (projectTask == null) ||
                    ((shotgunTask["due_date"] == null) && (projectTask.Finish != null)) ||
                    ((shotgunTask["due_date"] != null) && (projectTask.Finish == null)) ||
                    ((shotgunTask["due_date"] != null) && (projectTask.Finish != null) && ((DateTime)shotgunTask["due_date"] != (DateTime)(projectTask.Finish)));
                return _end;
            }
        }

        public Hashtable duration
        {
            get
            {
                Hashtable _duration = new Hashtable();
                int minutes = 0;
                if (mode == MODES.PullFromShotgun && (projectTask == null || !projectTask.Manual) && shotgunTask != null && shotgunTask["duration"] != null)
                    minutes = (int)(shotgunTask["duration"]);
                else if (projectTask != null && projectTask.Duration != null)
                    minutes = projectTask.Duration;
                int days = (int)(minutes / 60.0 / 8.0);
                _duration["value"] = String.Format("{0}", days);
                _duration["errored"] = false;
                _duration["different"] = ((projectTask == null && shotgunTask != null) || 
                                           (projectTask != null && shotgunTask == null) ||
                                          (projectTask != null && shotgunTask != null && shotgunTask["duration"] != null &&
                                             (int)(shotgunTask["duration"]) != projectTask.Duration) );
                return _duration;
            }
        }

        public Hashtable unit
        {
            get
            {
                Hashtable _unit = new Hashtable();
                String shotgunUnit = "";
                if ((shotgunTask != null) && (shotgunTask["sg_unit"] != IronPython.Modules.Builtin.None)) {
                    shotgunUnit = (String)((PythonDictionary)shotgunTask["sg_unit"])["name"];
                }
                String projectUnit = (projectTask == null) ? "" : projectTask.GetField(projectTask.Application.FieldNameToFieldConstant("Unit"));
                if (mode == MODES.PullFromShotgun) {
                    _unit["value"] = (shotgunTask == null) ? projectUnit : shotgunUnit;
                    _unit["errored"] = false;
                }
                if (mode == MODES.PushToShotgun) {
                    _unit["value"] = (projectTask == null) ? shotgunUnit : projectUnit;
                    _unit["errored"] = ((_unit["value"] as String) != "") && (!shotgunData.ContainsKey("units") || (shotgunData["units"].has_key(projectUnit) == false));
                }
                _unit["different"] = (projectUnit != shotgunUnit);
                return _unit;
            }
        }

        public Hashtable shot
        {
            get
            {
                Hashtable _shot = new Hashtable();
                String shotgunShot = "";
                if ((shotgunTask != null) && (shotgunTask["entity"] != IronPython.Modules.Builtin.None)) {
                    shotgunShot = (String)((PythonDictionary)shotgunTask["entity"])["name"];
                }
                String projectShot = (projectTask == null) ? "" : projectTask.GetField(projectTask.Application.FieldNameToFieldConstant("Shot"));
                if (mode == MODES.PullFromShotgun) {
                    _shot["value"] = (shotgunTask == null) ? projectShot : shotgunShot;
                    _shot["errored"] = false;
                }
                if (mode == MODES.PushToShotgun) {
                    _shot["value"] = (projectTask == null) ? shotgunShot : projectShot;
                    _shot["errored"] = !shotgunData.ContainsKey("shots") || (shotgunData["shots"].has_key(projectShot) == false);
                }
                _shot["different"] = (projectShot != shotgunShot);
                return _shot;
            }
        }

        public Hashtable sets
        {
            get
            {
                Hashtable _sets = new Hashtable();
                String projectSets = (projectTask == null) ? "" : projectTask.GetField(projectTask.Application.FieldNameToFieldConstant("Set"));
                List<String> projectSetNames = projectSets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                projectSetNames.Sort();
                projectSets = String.Join(", ", from s in projectSetNames select s.Trim());

                List<String> shotgunSetNames = new List<String>();
                if (shotgunTask != null) {
                    foreach (PythonDictionary shotgunSet in (IronPython.Runtime.List)shotgunTask["sg_set_builds"]) {
                        shotgunSetNames.Add((String)shotgunSet["name"]);
                    }
                }
                shotgunSetNames.Sort();
                String shotgunSets = String.Join(", ", from s in shotgunSetNames select s.Trim());

                if (mode == MODES.PullFromShotgun) {
                    _sets["value"] = (shotgunTask == null) ? projectSets : shotgunSets;
                    _sets["errored"] = false;
                }
                if (mode == MODES.PushToShotgun) {
                    _sets["value"] = (projectTask == null) ? shotgunSets : projectSets;
                    _sets["errored"] = false;
                    foreach (String projectSetName in projectSetNames) {
                        if (!shotgunData.ContainsKey("sets") || (shotgunData["sets"].has_key(projectSetName) == false)) {
                            _sets["errored"] = true;
                            break;
                        }
                    }
                }
                _sets["different"] = (projectSets != shotgunSets);

                return _sets;
            }
        }

        public Hashtable setBuilds
        {
            get
            {
                Hashtable _setBuilds = new Hashtable();
                String projectSetBuilds = (projectTask == null) ? "" : projectTask.GetField(projectTask.Application.FieldNameToFieldConstant("Set Builds"));
                List<String> projectSetBuildNames = projectSetBuilds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                projectSetBuildNames.Sort();
                projectSetBuilds = String.Join(", ", from s in projectSetBuildNames select s.Trim());

                List<String> shotgunSetBuildNames = new List<String>();
                if (shotgunTask != null)
                {
                    foreach (PythonDictionary shotgunSet in (IronPython.Runtime.List)shotgunTask["sg_set_phases"])
                    {
                        shotgunSetBuildNames.Add((String)shotgunSet["name"]);
                    }
                }
                shotgunSetBuildNames.Sort();
                String shotgunSetBuilds = String.Join(", ", from s in shotgunSetBuildNames select s.Trim());

                if (mode == MODES.PullFromShotgun)
                {
                    _setBuilds["value"] = (shotgunTask == null) ? projectSetBuilds : shotgunSetBuilds;
                    _setBuilds["errored"] = false;
                }
                if (mode == MODES.PushToShotgun)
                {
                    _setBuilds["value"] = (projectTask == null) ? shotgunSetBuilds : projectSetBuilds;
                    _setBuilds["errored"] = false;
                    foreach (String projectSetBuildName in projectSetBuildNames)
                    {
                        if (!shotgunData.ContainsKey("setBuilds") || (shotgunData["setBuilds"].has_key(projectSetBuildName) == false))
                        {
                            _setBuilds["errored"] = true;
                            break;
                        }
                    }
                }
                _setBuilds["different"] = (projectSetBuilds != shotgunSetBuilds);

                return _setBuilds;
            }
        }

        public Hashtable characters
        {
            get
            {
                Hashtable _characters = new Hashtable();
                String projectCharacters = (projectTask == null) ? "" : projectTask.GetField(projectTask.Application.FieldNameToFieldConstant("Characters"));
                List<String> projectCharacterNames = projectCharacters.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                projectCharacterNames.Sort();
                projectCharacters = String.Join(",", from s in projectCharacterNames where s.Trim() != overflowMarker select s.Trim());

                List<String> shotgunCharacterNames = new List<String>();
                String prefix = "Puppet ";
                if (shotgunTask != null) {
                    foreach (PythonDictionary shotgunCharacter in (IronPython.Runtime.List)shotgunTask["sg_puppets"]) {
                        String shotgunCharacterName = (String)shotgunCharacter["name"];
                        // Strip off the "Puppet " prefix
                        shotgunCharacterNames.Add(shotgunCharacterName.StartsWith(prefix) ? shotgunCharacterName.Substring(prefix.Length - 1) : shotgunCharacterName);
                    }
                }
                shotgunCharacterNames.Sort();
                String shotgunCharacters = String.Join(",", from s in shotgunCharacterNames select s.Trim());

                if (mode == MODES.PullFromShotgun) {
                    _characters["value"] = (shotgunTask == null) ? projectCharacters : shotgunCharacters;
                    _characters["errored"] = false;
                }
                if (mode == MODES.PushToShotgun) {
                    _characters["value"] = (projectTask == null) ? shotgunCharacters : projectCharacters;
                    _characters["errored"] = false;
                    foreach (String projectCharacterName in projectCharacterNames) {
                        if (!shotgunData.ContainsKey("characters") || (shotgunData["characters"].has_key(projectCharacterName) == false)) {
                            _characters["errored"] = true;
                            break;
                        }
                    }
                }
                _characters["different"] = (projectCharacters != shotgunCharacters);
                /*
                List<String> modifiedPuppetNames = new List<String>();
                
                if (mode == MODES.PullFromShotgun)
                {
                    // Strip off the common prefix on all puppets "Puppet "
                    foreach (String puppetName in ((String)_characters["value"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        modifiedPuppetNames.Add(puppetName.StartsWith(prefix) ? puppetName.Substring(prefix.Length - 1) : puppetName);
                    _characters["value"] = String.Join(",", modifiedPuppetNames.ToArray());
                    _characters["different"] = (projectCharacters != shotgunCharacters);
                }
                else
                {
                    // Pushing to shotgun, need to add "Puppet " back on
                    foreach (String puppetName in ((String)_characters["value"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        modifiedPuppetNames.Add(puppetName.StartsWith(prefix) ? puppetName : prefix + puppetName);
                    _characters["value"] = String.Join(",", modifiedPuppetNames.ToArray());
                    _characters["different"] = (_characters["value"] != projectCharacters != shotgunCharacters);

                }
                */
                return _characters;
            }
        }
    }
}
