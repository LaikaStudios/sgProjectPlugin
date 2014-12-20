namespace sg_prj
{
    partial class PushToShotgunForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Ok = new System.Windows.Forms.Button();
            this.taskGrid = new System.Windows.Forms.DataGridView();
            this.SyncColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShotColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetBuildsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CharactersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.syncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cancel = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.StatusStrip();
            this.label = new System.Windows.Forms.ToolStripStatusLabel();
            this.options = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.syncLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.syncDates = new System.Windows.Forms.CheckBox();
            this.syncUnits = new System.Windows.Forms.CheckBox();
            this.syncCharacters = new System.Windows.Forms.CheckBox();
            this.syncSets = new System.Windows.Forms.CheckBox();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.syncSetBuilds = new System.Windows.Forms.CheckBox();
            this.syncDurations = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).BeginInit();
            this.gridContextMenu.SuspendLayout();
            this.status.SuspendLayout();
            this.options.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Ok, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.taskGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Cancel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.status, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.options, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(907, 688);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.Ok.Enabled = false;
            this.Ok.Location = new System.Drawing.Point(375, 642);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 1;
            this.Ok.TabStop = false;
            this.Ok.Text = "&Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // taskGrid
            // 
            this.taskGrid.AllowUserToAddRows = false;
            this.taskGrid.AllowUserToDeleteRows = false;
            this.taskGrid.AllowUserToOrderColumns = true;
            this.taskGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Format = "MM/dd/yy";
            this.taskGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.taskGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SyncColumn,
            this.StatusColumn,
            this.ShotColumn,
            this.TaskColumn,
            this.UnitColumn,
            this.StartColumn,
            this.EndColumn,
            this.DurationColumn,
            this.SetsColumn,
            this.SetBuildsColumn,
            this.CharactersColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.taskGrid, 4);
            this.taskGrid.ContextMenuStrip = this.gridContextMenu;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "MM/dd/yy";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.taskGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.taskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.taskGrid.Location = new System.Drawing.Point(3, 3);
            this.taskGrid.Name = "taskGrid";
            this.taskGrid.RowHeadersVisible = false;
            this.taskGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.taskGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.taskGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.taskGrid.Size = new System.Drawing.Size(901, 553);
            this.taskGrid.TabIndex = 3;
            // 
            // SyncColumn
            // 
            this.SyncColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SyncColumn.HeaderText = "Sync";
            this.SyncColumn.Name = "SyncColumn";
            this.SyncColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SyncColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SyncColumn.Width = 56;
            // 
            // StatusColumn
            // 
            this.StatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StatusColumn.HeaderText = "Status";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.Width = 62;
            // 
            // ShotColumn
            // 
            this.ShotColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShotColumn.HeaderText = "Shot";
            this.ShotColumn.Name = "ShotColumn";
            this.ShotColumn.ReadOnly = true;
            this.ShotColumn.Width = 54;
            // 
            // TaskColumn
            // 
            this.TaskColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TaskColumn.FillWeight = 65.65144F;
            this.TaskColumn.HeaderText = "Task";
            this.TaskColumn.Name = "TaskColumn";
            this.TaskColumn.ReadOnly = true;
            this.TaskColumn.Width = 56;
            // 
            // UnitColumn
            // 
            this.UnitColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UnitColumn.FillWeight = 203.0457F;
            this.UnitColumn.HeaderText = "Unit";
            this.UnitColumn.Name = "UnitColumn";
            this.UnitColumn.ReadOnly = true;
            this.UnitColumn.Width = 51;
            // 
            // StartColumn
            // 
            this.StartColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StartColumn.FillWeight = 65.65144F;
            this.StartColumn.HeaderText = "Start";
            this.StartColumn.Name = "StartColumn";
            this.StartColumn.ReadOnly = true;
            this.StartColumn.Width = 54;
            // 
            // EndColumn
            // 
            this.EndColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EndColumn.FillWeight = 65.65144F;
            this.EndColumn.HeaderText = "End";
            this.EndColumn.Name = "EndColumn";
            this.EndColumn.ReadOnly = true;
            this.EndColumn.Width = 51;
            // 
            // DurationColumn
            // 
            this.DurationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DurationColumn.HeaderText = "Duration";
            this.DurationColumn.Name = "DurationColumn";
            this.DurationColumn.ReadOnly = true;
            this.DurationColumn.Width = 72;
            // 
            // SetsColumn
            // 
            this.SetsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SetsColumn.HeaderText = "Sets";
            this.SetsColumn.Name = "SetsColumn";
            this.SetsColumn.ReadOnly = true;
            this.SetsColumn.Width = 53;
            // 
            // SetBuildsColumn
            // 
            this.SetBuildsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SetBuildsColumn.HeaderText = "Set Builds";
            this.SetBuildsColumn.Name = "SetBuildsColumn";
            this.SetBuildsColumn.ReadOnly = true;
            this.SetBuildsColumn.Width = 79;
            // 
            // CharactersColumn
            // 
            this.CharactersColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CharactersColumn.HeaderText = "Characters";
            this.CharactersColumn.Name = "CharactersColumn";
            this.CharactersColumn.ReadOnly = true;
            this.CharactersColumn.Width = 83;
            // 
            // gridContextMenu
            // 
            this.gridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncToolStripMenuItem,
            this.noSyncToolStripMenuItem});
            this.gridContextMenu.Name = "gridContextMenu";
            this.gridContextMenu.Size = new System.Drawing.Size(157, 48);
            // 
            // syncToolStripMenuItem
            // 
            this.syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            this.syncToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.syncToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.syncToolStripMenuItem.Text = "Sync";
            this.syncToolStripMenuItem.Click += new System.EventHandler(this.toggleSyncMenuItem_Click);
            // 
            // noSyncToolStripMenuItem
            // 
            this.noSyncToolStripMenuItem.Name = "noSyncToolStripMenuItem";
            this.noSyncToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.noSyncToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.noSyncToolStripMenuItem.Text = "No Sync";
            this.noSyncToolStripMenuItem.Click += new System.EventHandler(this.noSyncToolStripMenuItem_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(456, 642);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.TabStop = false;
            this.Cancel.Text = "&Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // status
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.status, 4);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label});
            this.status.Location = new System.Drawing.Point(0, 668);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(907, 20);
            this.status.SizingGrip = false;
            this.status.TabIndex = 4;
            // 
            // label
            // 
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 15);
            // 
            // options
            // 
            this.options.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.options, 4);
            this.options.Controls.Add(this.tableLayoutPanel2);
            this.options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.options.Location = new System.Drawing.Point(3, 562);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(901, 74);
            this.options.TabIndex = 5;
            this.options.TabStop = false;
            this.options.Text = "Options";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 14;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tableLayoutPanel2.Controls.Add(this.syncLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.filterTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.syncDates, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.syncUnits, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.syncCharacters, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.syncSets, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.toDate, 13, 1);
            this.tableLayoutPanel2.Controls.Add(this.fromDate, 11, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 12, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 10, 1);
            this.tableLayoutPanel2.Controls.Add(this.syncSetBuilds, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.syncDurations, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(895, 55);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // syncLabel
            // 
            this.syncLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.syncLabel.AutoSize = true;
            this.syncLabel.Location = new System.Drawing.Point(3, 3);
            this.syncLabel.Margin = new System.Windows.Forms.Padding(3);
            this.syncLabel.Name = "syncLabel";
            this.syncLabel.Size = new System.Drawing.Size(33, 17);
            this.syncLabel.TabIndex = 11;
            this.syncLabel.Text = "Label";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filter:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filterTextBox
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.filterTextBox, 7);
            this.filterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterTextBox.Location = new System.Drawing.Point(42, 26);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(482, 20);
            this.filterTextBox.TabIndex = 12;
            this.filterTextBox.WordWrap = false;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            // 
            // syncDates
            // 
            this.syncDates.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.syncDates.AutoSize = true;
            this.syncDates.Location = new System.Drawing.Point(42, 3);
            this.syncDates.Name = "syncDates";
            this.syncDates.Size = new System.Drawing.Size(54, 17);
            this.syncDates.TabIndex = 6;
            this.syncDates.Text = "Dates";
            this.syncDates.UseVisualStyleBackColor = true;
            this.syncDates.CheckedChanged += new System.EventHandler(this.syncDates_CheckedChanged);
            // 
            // syncUnits
            // 
            this.syncUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.syncUnits.AutoSize = true;
            this.syncUnits.Location = new System.Drawing.Point(102, 3);
            this.syncUnits.Name = "syncUnits";
            this.syncUnits.Size = new System.Drawing.Size(50, 17);
            this.syncUnits.TabIndex = 7;
            this.syncUnits.Text = "Units";
            this.syncUnits.UseVisualStyleBackColor = true;
            this.syncUnits.CheckedChanged += new System.EventHandler(this.syncUnits_CheckedChanged);
            // 
            // syncCharacters
            // 
            this.syncCharacters.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.syncCharacters.AutoSize = true;
            this.syncCharacters.Location = new System.Drawing.Point(290, 3);
            this.syncCharacters.Name = "syncCharacters";
            this.syncCharacters.Size = new System.Drawing.Size(77, 17);
            this.syncCharacters.TabIndex = 10;
            this.syncCharacters.Text = "Characters";
            this.syncCharacters.UseVisualStyleBackColor = true;
            this.syncCharacters.CheckedChanged += new System.EventHandler(this.syncCharacters_CheckedChanged);
            // 
            // syncSets
            // 
            this.syncSets.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.syncSets.AutoSize = true;
            this.syncSets.Location = new System.Drawing.Point(158, 3);
            this.syncSets.Name = "syncSets";
            this.syncSets.Size = new System.Drawing.Size(47, 17);
            this.syncSets.TabIndex = 8;
            this.syncSets.Text = "Sets";
            this.syncSets.UseVisualStyleBackColor = true;
            this.syncSets.CheckedChanged += new System.EventHandler(this.syncSets_CheckedChanged);
            // 
            // toDate
            // 
            this.toDate.CustomFormat = " MM/dd/yy";
            this.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate.Location = new System.Drawing.Point(732, 26);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(100, 20);
            this.toDate.TabIndex = 14;
            this.toDate.Value = new System.DateTime(2012, 5, 24, 0, 0, 0, 0);
            this.toDate.ValueChanged += new System.EventHandler(this.toDate_ValueChanged);
            // 
            // fromDate
            // 
            this.fromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.fromDate.CustomFormat = " MM/dd/yy";
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate.Location = new System.Drawing.Point(574, 26);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(101, 20);
            this.fromDate.TabIndex = 13;
            this.fromDate.Value = new System.DateTime(2012, 5, 24, 0, 0, 0, 0);
            this.fromDate.ValueChanged += new System.EventHandler(this.fromDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(701, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 32);
            this.label3.TabIndex = 16;
            this.label3.Text = "To:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(530, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 32);
            this.label2.TabIndex = 14;
            this.label2.Text = "From:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // syncSetBuilds
            // 
            this.syncSetBuilds.AutoSize = true;
            this.syncSetBuilds.Location = new System.Drawing.Point(211, 3);
            this.syncSetBuilds.Name = "syncSetBuilds";
            this.syncSetBuilds.Size = new System.Drawing.Size(73, 17);
            this.syncSetBuilds.TabIndex = 9;
            this.syncSetBuilds.Text = "Set Builds";
            this.syncSetBuilds.UseVisualStyleBackColor = true;
            this.syncSetBuilds.CheckedChanged += new System.EventHandler(this.syncSetBuilds_CheckedChanged);
            // 
            // syncDurations
            // 
            this.syncDurations.AutoSize = true;
            this.syncDurations.Location = new System.Drawing.Point(373, 3);
            this.syncDurations.Name = "syncDurations";
            this.syncDurations.Size = new System.Drawing.Size(71, 17);
            this.syncDurations.TabIndex = 11;
            this.syncDurations.Text = "Durations";
            this.syncDurations.UseVisualStyleBackColor = true;
            this.syncDurations.CheckedChanged += new System.EventHandler(this.syncDurations_CheckedChanged);
            // 
            // PushToShotgunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(907, 688);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PushToShotgunForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.PushToShotgunForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).EndInit();
            this.gridContextMenu.ResumeLayout(false);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.options.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.DataGridView taskGrid;
        public System.Windows.Forms.StatusStrip status;
        public System.Windows.Forms.ToolStripStatusLabel label;
        public System.Windows.Forms.Button Ok;
        public System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox options;
        private System.Windows.Forms.ContextMenuStrip gridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem syncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noSyncToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.Label syncLabel;
        public System.Windows.Forms.CheckBox syncDates;
        public System.Windows.Forms.CheckBox syncUnits;
        public System.Windows.Forms.CheckBox syncCharacters;
        public System.Windows.Forms.CheckBox syncSets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker fromDate;
        public System.Windows.Forms.DateTimePicker toDate;
        public System.Windows.Forms.CheckBox syncSetBuilds;
        public System.Windows.Forms.CheckBox syncDurations;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SyncColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShotColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DurationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetBuildsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CharactersColumn;
    }
}