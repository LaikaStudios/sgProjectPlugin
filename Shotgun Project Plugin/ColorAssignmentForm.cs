using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MSProject = Microsoft.Office.Interop.MSProject;

namespace sg_prj
{
    public partial class ColorAssignmentForm : Form
    {
        public ColorAssignmentForm() {
            InitializeComponent();
        }

        private void ResourceGroupCombo_SelectedIndexChanged(object sender, EventArgs e) {
            // Grab the fields we'll want to pull
            MSProject.PjField colorFieldId =
                Globals.TasksManagerAddIn.Application.FieldNameToFieldConstant(Globals.TasksManagerAddIn.resourceColorField, MSProject.PjFieldType.pjResource);
            MSProject.PjField borderColorFieldId =
                Globals.TasksManagerAddIn.Application.FieldNameToFieldConstant(Globals.TasksManagerAddIn.resourceBorderColorField, MSProject.PjFieldType.pjResource);
            // Initialize the grid with the existing data
            String group = this.ResourceGroupCombo.SelectedItem.ToString();
            DataGridViewRowCollection rows = this.ResourceColorGrid.Rows;
            rows.Clear();
            int row = 0;
            foreach (MSProject.Resource res in Globals.TasksManagerAddIn.Application.ActiveProject.Resources) {
                if (res.Group != group)
                    continue;
                rows.Add(res.Name);
                rows[row].Cells[ColorColumn.Index].Style.BackColor =
                    Globals.TasksManagerAddIn.stringToColor(res.GetField(colorFieldId), Color.White);
                rows[row].Cells[ColorColumn.Index].Style.SelectionBackColor =
                    Globals.TasksManagerAddIn.stringToColor(res.GetField(colorFieldId), Color.White);
                rows[row].Cells[BorderColorColumn.Index].Style.BackColor =
                    Globals.TasksManagerAddIn.stringToColor(res.GetField(borderColorFieldId), Color.Black);
                rows[row].Cells[BorderColorColumn.Index].Style.SelectionBackColor =
                    Globals.TasksManagerAddIn.stringToColor(res.GetField(borderColorFieldId), Color.Black);
                row++;
            }
        }

        private void OkButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void ResourceColorGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            // Grab the fields we'll want to push
            MSProject.PjField fieldId;
            Color defaultColor;
            if (e.ColumnIndex == ColorColumn.Index) {
                fieldId = Globals.TasksManagerAddIn.Application.FieldNameToFieldConstant(Globals.TasksManagerAddIn.resourceColorField, MSProject.PjFieldType.pjResource);
                defaultColor = Color.White;
            } else if (e.ColumnIndex == BorderColorColumn.Index) {
                fieldId = Globals.TasksManagerAddIn.Application.FieldNameToFieldConstant(Globals.TasksManagerAddIn.resourceBorderColorField, MSProject.PjFieldType.pjResource);
                defaultColor = Color.Black;
            }  else
                return;
            // Find the res
            MSProject.Resource res =
                Globals.TasksManagerAddIn.Application.ActiveProject.Resources[this.ResourceColorGrid.Rows[e.RowIndex].Cells[NameColumn.Index].Value];
            this.colorDialog.Color = Globals.TasksManagerAddIn.stringToColor(res.GetField(fieldId), defaultColor);
            // Set the value
            System.Windows.Forms.DialogResult result = this.colorDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            Color c = this.colorDialog.Color;
            this.ResourceColorGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = c;
            this.ResourceColorGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = c;
            res.SetField(fieldId, String.Format("{0:X2} {1:X2} {2:X2}", c.R, c.G, c.B));
        }

        private void ResourceColorGrid_VisibleChanged(object sender, EventArgs e) {
            this.ResourceGroupCombo.SelectedIndex = 0;
        }
    }
}
