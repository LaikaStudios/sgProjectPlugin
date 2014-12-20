using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace sg_prj
{
    public partial class ShotgunRibbon
    {
        private void ShotgunRibbon_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void pushToResourcesButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.PushToResources();
        }

        private void PullFromResources_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.PullFromResources();
        }

        private void UpdateDeliveries_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.UpdateDeliveries();
        }

        private void ResetAvailableUnits_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ResetAvailableUnits();
        }

        private void ColorBySet_Click(object sender, RibbonControlEventArgs e) {
            Globals.ThisAddIn.ColorByResource("Set");
        }

        private void ColorBySequence_Click(object sender, RibbonControlEventArgs e) {
            Globals.ThisAddIn.ColorByResource("Sequence");
        }

        private void ColorByUnit_Click(object sender, RibbonControlEventArgs e) {
            Globals.ThisAddIn.ColorByResource("Unit");
        }

        private void AssignColors_Click(object sender, RibbonControlEventArgs e) {
            ColorAssignmentForm form = new ColorAssignmentForm();
            form.Show();
        }

        private void PushToShotgun_Click(object sender, RibbonControlEventArgs e) {
            Globals.ThisAddIn.PushToShotgun();
        }

        private void PullFromShotgun_Click(object sender, RibbonControlEventArgs e) {
            Globals.ThisAddIn.PullFromShotgun();
        }

        private void About_Click(object sender, RibbonControlEventArgs e) {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }
    }
}