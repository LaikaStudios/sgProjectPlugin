namespace sg_prj
{
    partial class ColorAssignmentForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.ResourceColorGrid = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BorderColorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OkButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ResourceGroupCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceColorGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // ResourceColorGrid
            // 
            this.ResourceColorGrid.AllowUserToAddRows = false;
            this.ResourceColorGrid.AllowUserToDeleteRows = false;
            this.ResourceColorGrid.AllowUserToResizeColumns = false;
            this.ResourceColorGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ResourceColorGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ResourceColorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResourceColorGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.ColorColumn,
            this.BorderColorColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.ResourceColorGrid, 3);
            this.ResourceColorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResourceColorGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ResourceColorGrid.Location = new System.Drawing.Point(3, 30);
            this.ResourceColorGrid.MultiSelect = false;
            this.ResourceColorGrid.Name = "ResourceColorGrid";
            this.ResourceColorGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResourceColorGrid.ShowCellErrors = false;
            this.ResourceColorGrid.ShowCellToolTips = false;
            this.ResourceColorGrid.ShowRowErrors = false;
            this.ResourceColorGrid.Size = new System.Drawing.Size(384, 547);
            this.ResourceColorGrid.TabIndex = 0;
            this.ResourceColorGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResourceColorGrid_CellDoubleClick);
            this.ResourceColorGrid.VisibleChanged += new System.EventHandler(this.ResourceColorGrid_VisibleChanged);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // ColorColumn
            // 
            this.ColorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColorColumn.HeaderText = "Color";
            this.ColorColumn.Name = "ColorColumn";
            this.ColorColumn.ReadOnly = true;
            this.ColorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColorColumn.Width = 80;
            // 
            // BorderColorColumn
            // 
            this.BorderColorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BorderColorColumn.HeaderText = "Border Color";
            this.BorderColorColumn.Name = "BorderColorColumn";
            this.BorderColorColumn.ReadOnly = true;
            this.BorderColorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BorderColorColumn.Width = 80;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.OkButton, 3);
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(312, 583);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "&Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.OkButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ResourceColorGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ResourceGroupCombo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 609);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // ResourceGroupCombo
            // 
            this.ResourceGroupCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ResourceGroupCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ResourceGroupCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResourceGroupCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResourceGroupCombo.FormattingEnabled = true;
            this.ResourceGroupCombo.Items.AddRange(new object[] {
            "Set",
            "Sequence",
            "Unit"});
            this.ResourceGroupCombo.Location = new System.Drawing.Point(135, 3);
            this.ResourceGroupCombo.Name = "ResourceGroupCombo";
            this.ResourceGroupCombo.Size = new System.Drawing.Size(121, 21);
            this.ResourceGroupCombo.TabIndex = 2;
            this.ResourceGroupCombo.SelectedIndexChanged += new System.EventHandler(this.ResourceGroupCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "Resource Type To Color:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorAssignmentForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 609);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ColorAssignmentForm";
            this.ShowInTaskbar = false;
            this.Text = "Resource Colors";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.ResourceColorGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox ResourceGroupCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BorderColorColumn;
        public System.Windows.Forms.DataGridView ResourceColorGrid;


    }
}