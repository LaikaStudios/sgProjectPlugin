namespace sg_prj {
    partial class ShotgunProjectsForm {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Ok = new System.Windows.Forms.Button();
            this.projectsGrid = new System.Windows.Forms.DataGridView();
            this.ProjectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.Ok, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.projectsGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Cancel, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 262);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.Ok.Location = new System.Drawing.Point(64, 236);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 4;
            this.Ok.Text = "&Ok";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // projectsGrid
            // 
            this.projectsGrid.AllowUserToAddRows = false;
            this.projectsGrid.AllowUserToDeleteRows = false;
            this.projectsGrid.AllowUserToResizeRows = false;
            this.projectsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projectsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.projectsGrid, 4);
            this.projectsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.projectsGrid.Location = new System.Drawing.Point(3, 3);
            this.projectsGrid.MultiSelect = false;
            this.projectsGrid.Name = "projectsGrid";
            this.projectsGrid.ReadOnly = true;
            this.projectsGrid.RowHeadersVisible = false;
            this.projectsGrid.RowTemplate.ReadOnly = true;
            this.projectsGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.projectsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.projectsGrid.Size = new System.Drawing.Size(278, 227);
            this.projectsGrid.TabIndex = 6;
            // 
            // ProjectColumn
            // 
            this.ProjectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProjectColumn.FillWeight = 65.65144F;
            this.ProjectColumn.HeaderText = "Project";
            this.ProjectColumn.Name = "ProjectColumn";
            this.ProjectColumn.ReadOnly = true;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(145, 236);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "&Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // ShotgunProjectsForm
            // 
            this.AcceptButton = this.Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ShotgunProjectsForm";
            this.ShowInTaskbar = false;
            this.Text = "Shotgun Projects";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ShotgunProjectsForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Ok;
        public System.Windows.Forms.DataGridView projectsGrid;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectColumn;
    }
}