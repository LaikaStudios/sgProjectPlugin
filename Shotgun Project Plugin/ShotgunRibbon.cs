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

        private void PushToResourcesButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.TasksManagerAddIn.PushToResources();
        }

        private void UpdateDeliveries_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.TasksManagerAddIn.UpdateDeliveries();
        }

        private void PushToShotgun_Click(object sender, RibbonControlEventArgs e) {
            Globals.TasksManagerAddIn.PushToShotgun();
        }

        private void PullFromShotgun_Click(object sender, RibbonControlEventArgs e) {
            Globals.TasksManagerAddIn.PullFromShotgun();
        }

        private void About_Click(object sender, RibbonControlEventArgs e) {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        private void SetShotgunProject_Click(object sender, RibbonControlEventArgs e) {
            Globals.TasksManagerAddIn.SetShotgunProject();
        }

        private void about_Click_1(object sender, RibbonControlEventArgs e) {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }
    }
}