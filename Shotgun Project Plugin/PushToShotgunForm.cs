using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sg_prj {
    public partial class PushToShotgunForm : Form {
        private Dictionary<int, CheckBox> syncCheckboxes;
        private Dictionary<String, int> statusCounts;

        public PushToShotgunForm()
        {
            InitializeComponent();
            syncCheckboxes = new Dictionary<int, CheckBox>() {
                {StartColumn.Index, syncDates},
                {EndColumn.Index, syncDates},
                {UnitColumn.Index, syncUnits},
                {SetsColumn.Index, syncSets},
                {SetBuildsColumn.Index, syncSetBuilds},
                {CharactersColumn.Index, syncCharacters},
                {DurationColumn.Index, syncDurations},
            };
            statusCounts = new Dictionary<String, int> {
                {"Total",  0},
                {"Error",  0},
                {"Omit",   0}, // Omit from Shotgun
                {"Remove", 0}, // Delete from Project
                {"Create", 0},
                {"Update", 0},
                {"Skip",   0},
            };
        }

        public Dictionary<String, int> getStatusCounts() {
            return statusCounts;
        }

        private void PushToShotgunForm_Load(object sender, EventArgs e)
        {
            ActiveControl = taskGrid;
            toDate.ValueChanged += new EventHandler(toDate_ValueChanged);
            fromDate.ValueChanged += new EventHandler(fromDate_ValueChanged);
            taskGrid.CurrentCellDirtyStateChanged += new EventHandler(taskGrid_CurrentCellDirtyStateChanged);
            taskGrid.CellValueChanged += new DataGridViewCellEventHandler(taskGrid_CellValueChanged);
            taskGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(taskGrid_RowsAdded);
            TaskRow.sync =
                (syncDates.Checked ? TaskRow.SYNC_FIELDS.Dates : 0) |
                (syncUnits.Checked ? TaskRow.SYNC_FIELDS.Units : 0) |
                (syncSets.Checked ? TaskRow.SYNC_FIELDS.Sets : 0) |
                (syncSetBuilds.Checked ? TaskRow.SYNC_FIELDS.SetBuilds : 0) |
                (syncCharacters.Checked ? TaskRow.SYNC_FIELDS.Characters : 0) |
                (syncDurations.Checked ? TaskRow.SYNC_FIELDS.Durations : 0);
        }

        private void toggleSyncMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in taskGrid.SelectedRows) {
                if (row.Visible)
                    row.Cells[0].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void noSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in taskGrid.SelectedRows) {
                if (row.Visible)
                    row.Cells[0].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            updateFilteredRows();
        }

        private void toDate_ValueChanged(object sender, EventArgs e)
        {
            updateFilteredRows();
        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            updateFilteredRows();
        }

        private bool filterOutRow(DataGridViewRow row)
        {
            bool filterOut = false;
            if (!filterOut) {
                if ((row.Cells[StartColumn.Index].Value != null) && ((DateTime)row.Cells[StartColumn.Index].Value > toDate.Value))
                    filterOut = true;
            }
            if (!filterOut) {
                if ((row.Cells[EndColumn.Index].Value != null) && ((DateTime)row.Cells[EndColumn.Index].Value < fromDate.Value))
                    filterOut = true;
            }
            if (!filterOut) {
                String[] comps = filterTextBox.Text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String comp in comps) {
                    bool found_comp = false;
                    foreach (DataGridViewCell cell in row.Cells) {
                        if ((cell.ColumnIndex == StartColumn.Index) || (cell.ColumnIndex == EndColumn.Index))
                            continue;
                        if ((cell.Value != null) && cell.Value.ToString().ToLower().Contains(comp)) {
                            found_comp = true;
                            break;
                        }
                    }
                    if (!found_comp) {
                        filterOut = true;
                        break;
                    }
                }
            }
            return filterOut;
        }

        private void updateFilteredRows()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in taskGrid.Rows)
                rows.Add(row);
            taskGrid.Rows.Clear();
            foreach (DataGridViewRow row in rows)
                updateRowStatus(row);
            taskGrid.Rows.AddRange(rows.ToArray());
            Cursor.Current = Cursors.Default;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void UpdateColumn(int column)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in taskGrid.Rows)
                rows.Add(row);
            taskGrid.Rows.Clear();
            foreach (DataGridViewRow row in rows) {
                bool sync = (row.Cells[SyncColumn.Index].Value != null) && ((bool)row.Cells[SyncColumn.Index].Value == true);
                updateCell(row.Cells[column], sync);
                updateRowStatus(row);
            }
            taskGrid.Rows.Clear();
            taskGrid.Rows.AddRange(rows.ToArray());
            Cursor.Current = Cursors.Default;
        }

        private void updateRowStatus(DataGridViewRow row, bool syncChanged = false)
        {
            TaskRow taskRow = row.Tag as TaskRow;
            String oldStatus = row.Cells[StatusColumn.Index].Value as String;
            String newStatus = taskRow.status;
            row.Cells[StatusColumn.Index].Value = newStatus;
            row.Cells[StatusColumn.Index].Style = null;
            row.Visible = (!filterOutRow(row) && (newStatus != "Skip"));
            bool sync = (row.Cells[SyncColumn.Index].Value != null) && ((bool)row.Cells[SyncColumn.Index].Value == true);
            if (sync == false) {
                row.Cells[StatusColumn.Index].Style.ForeColor = System.Drawing.Color.Gray;
                row.Cells[StatusColumn.Index].Style.SelectionForeColor = System.Drawing.Color.Gray;
            } else {
                switch (newStatus) {
                    case "Error":
                    case "Omit":
                    case "Remove":
                        row.Cells[StatusColumn.Index].Style.ForeColor = System.Drawing.Color.Red;
                        row.Cells[StatusColumn.Index].Style.SelectionForeColor = System.Drawing.Color.Red;
                        break;
                    case "Create":
                        row.Cells[StatusColumn.Index].Style.ForeColor = System.Drawing.Color.Green;
                        row.Cells[StatusColumn.Index].Style.SelectionForeColor = System.Drawing.Color.Green;
                        break;
                    case "Update":
                        row.Cells[StatusColumn.Index].Style.ForeColor = System.Drawing.Color.Blue;
                        row.Cells[StatusColumn.Index].Style.SelectionForeColor = System.Drawing.Color.Blue;
                        break;
                    case "Skip":
                        row.Cells[StatusColumn.Index].Style.ForeColor = System.Drawing.Color.MediumTurquoise;
                        row.Cells[StatusColumn.Index].Style.SelectionForeColor = System.Drawing.Color.MediumTurquoise;
                        break;
                    default:
                        throw new Exception("Unknown row status: " + status);
                }
            } // sync

            // update status bar
            if (oldStatus != newStatus)
            {
                if (oldStatus != null && statusCounts.ContainsKey(oldStatus) && (statusCounts[oldStatus] > 0))
                    statusCounts[oldStatus]--;
                statusCounts[newStatus]++;
            }
            else
            {
                if (syncChanged)
                {
                    int adjustment = (sync) ? 1 : -1;
                    statusCounts[newStatus] += adjustment;
                    statusCounts["Skip"] -= adjustment;
                }
            }
            status.Items[0].Text = String.Format(
                "{0} tasks.   {1} updates. {2} creates. {3} omits. {4} removes. {5} errors. {6} skipped.",
                statusCounts["Total"],
                statusCounts["Update"],
                statusCounts["Create"],
                statusCounts["Omit"],
                statusCounts["Remove"],
                statusCounts["Error"],
                statusCounts["Skip"]
            );
        }

        private void updateCell(DataGridViewCell cell, bool sync)
        {
            TaskRow taskRow = cell.OwningRow.Tag as TaskRow;
            System.Diagnostics.Debug.WriteLine(String.Format("Bitches {0}", cell.OwningColumn.HeaderText));
            if (taskRow.has_key(cell.OwningColumn.HeaderText)) {
                Hashtable data = taskRow[cell.OwningColumn.HeaderText];
                cell.Value = data["value"];
                cell.Style = null;
                if (sync == false) {
                    cell.Style.ForeColor = System.Drawing.Color.Gray;
                    cell.Style.SelectionForeColor = System.Drawing.Color.Gray;
                } else if (syncCheckboxes.ContainsKey(cell.OwningColumn.Index) && (syncCheckboxes[cell.OwningColumn.Index].Checked == false)) {
                    cell.Style.ForeColor = System.Drawing.Color.Gray;
                    cell.Style.SelectionForeColor = System.Drawing.Color.Gray;
                } else if ((bool)data["errored"] == true) {
                    cell.Style.ForeColor = System.Drawing.Color.Red;
                    cell.Style.SelectionForeColor = System.Drawing.Color.Red;
                    cell.Style.BackColor = System.Drawing.Color.Bisque;
                } else if ((bool)data["different"] == true) {
                    cell.Style.ForeColor = System.Drawing.Color.DarkGreen;
                    cell.Style.SelectionForeColor = System.Drawing.Color.DarkGreen;
                    cell.Style.BackColor = System.Drawing.Color.Honeydew;
                }
            }
        }

        public void initRow(DataGridViewRow row, IronPython.Runtime.PythonDictionary shotgunTask = null, Microsoft.Office.Interop.MSProject.Task projectTask = null)
        {
            row.Tag = new TaskRow(shotgunTaskIn: shotgunTask, projectTaskIn: projectTask);
        }

        public void updateRow(DataGridViewRow row, bool syncChanged = false)
        {
            updateRowStatus(row, syncChanged);
            bool sync = (row.Cells[SyncColumn.Index].Value != null) && ((bool)row.Cells[SyncColumn.Index].Value == true);
            foreach (DataGridViewCell cell in row.Cells)
                updateCell(cell, sync);
        }
        public void updateRows()
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in taskGrid.Rows)
                rows.Add(row);
            taskGrid.Rows.Clear();

            foreach (DataGridViewRow row in rows)
                updateRow(row);

            taskGrid.Rows.AddRange(rows.ToArray());
        }

        /*********************************************
         * Checkbox handlers
         *********************************************/
        private void syncUnits_CheckedChanged(object sender, EventArgs e)
        {
            if (syncUnits.Checked)
                TaskRow.sync |= TaskRow.SYNC_FIELDS.Units;
            else
                TaskRow.sync &= ~TaskRow.SYNC_FIELDS.Units;
            UpdateColumn(UnitColumn.Index);
        }

        private void syncSets_CheckedChanged(object sender, EventArgs e)
        {
            if (syncSets.Checked)
                TaskRow.sync |= TaskRow.SYNC_FIELDS.Sets;
            else
                TaskRow.sync &= ~TaskRow.SYNC_FIELDS.Sets;
            UpdateColumn(SetsColumn.Index);
        }

        private void syncSetBuilds_CheckedChanged(object sender, EventArgs e)
        {
            if (syncSetBuilds.Checked)
                TaskRow.sync |= TaskRow.SYNC_FIELDS.SetBuilds;
            else
                TaskRow.sync &= ~TaskRow.SYNC_FIELDS.SetBuilds;
            UpdateColumn(SetBuildsColumn.Index);
        }

        private void syncCharacters_CheckedChanged(object sender, EventArgs e)
        {
            if (syncCharacters.Checked)
                TaskRow.sync |= TaskRow.SYNC_FIELDS.Characters;
            else
                TaskRow.sync &= ~TaskRow.SYNC_FIELDS.Characters;
            UpdateColumn(CharactersColumn.Index);
        }

        private void syncDates_CheckedChanged(object sender, EventArgs e)
        {
            if (syncDates.Checked)
                TaskRow.sync |= TaskRow.SYNC_FIELDS.Dates;
            else
                TaskRow.sync &= ~TaskRow.SYNC_FIELDS.Dates;
            UpdateColumn(StartColumn.Index);
            UpdateColumn(EndColumn.Index);
        }

        private void taskGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == SyncColumn.Index)
                updateRow(taskGrid.Rows[e.RowIndex], syncChanged : true);
        }

        private void taskGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (taskGrid.IsCurrentCellDirty)
                taskGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void taskGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            statusCounts["Total"] = taskGrid.Rows.Count;
        }

        private void syncDurations_CheckedChanged(object sender, EventArgs e)
        {
            if (syncDurations.Checked)
                TaskRow.sync |= TaskRow.SYNC_FIELDS.Durations;
            else
                TaskRow.sync &= ~TaskRow.SYNC_FIELDS.Durations;
            UpdateColumn(DurationColumn.Index);
        }

    }
}
