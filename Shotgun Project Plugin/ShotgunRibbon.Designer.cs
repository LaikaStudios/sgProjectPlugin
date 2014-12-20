namespace sg_prj
{
    partial class ShotgunRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ShotgunRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ShotgunTab = this.Factory.CreateRibbonTab();
            this.ShotgunProjectPlugin = this.Factory.CreateRibbonGroup();
            this.PushToShotgun = this.Factory.CreateRibbonButton();
            this.PullFromShotgun = this.Factory.CreateRibbonButton();
            this.Advanced = this.Factory.CreateRibbonMenu();
            this.SetShotgunProject = this.Factory.CreateRibbonButton();
            this.PushToResourcesButton = this.Factory.CreateRibbonButton();
            this.UpdateDeliveries = this.Factory.CreateRibbonButton();
            this.about = this.Factory.CreateRibbonButton();
            this.ShotgunTab.SuspendLayout();
            this.ShotgunProjectPlugin.SuspendLayout();
            // 
            // ShotgunTab
            // 
            this.ShotgunTab.Groups.Add(this.ShotgunProjectPlugin);
            this.ShotgunTab.KeyTip = "SG";
            this.ShotgunTab.Label = "Shotgun";
            this.ShotgunTab.Name = "ShotgunTab";
            // 
            // ShotgunProjectPlugin
            // 
            this.ShotgunProjectPlugin.Items.Add(this.PushToShotgun);
            this.ShotgunProjectPlugin.Items.Add(this.PullFromShotgun);
            this.ShotgunProjectPlugin.Items.Add(this.Advanced);
            this.ShotgunProjectPlugin.Name = "ShotgunProjectPlugin";
            // 
            // PushToShotgun
            // 
            this.PushToShotgun.Description = "Push to Shotgun";
            this.PushToShotgun.Label = "Push to Shotgun...";
            this.PushToShotgun.Name = "PushToShotgun";
            this.PushToShotgun.OfficeImageId = "DatabaseSqlServer";
            this.PushToShotgun.ShowImage = true;
            this.PushToShotgun.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PushToShotgun_Click);
            // 
            // PullFromShotgun
            // 
            this.PullFromShotgun.Description = "Pull From Shotgun";
            this.PullFromShotgun.Label = "Pull from Shotgun...";
            this.PullFromShotgun.Name = "PullFromShotgun";
            this.PullFromShotgun.OfficeImageId = "FilesToolArrangeGallery";
            this.PullFromShotgun.ShowImage = true;
            this.PullFromShotgun.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PullFromShotgun_Click);
            // 
            // Advanced
            // 
            this.Advanced.Items.Add(this.SetShotgunProject);
            this.Advanced.Items.Add(this.PushToResourcesButton);
            this.Advanced.Items.Add(this.UpdateDeliveries);
            this.Advanced.Items.Add(this.about);
            this.Advanced.Label = "Advanced";
            this.Advanced.Name = "Advanced";
            this.Advanced.OfficeImageId = "AccessFormWizard";
            this.Advanced.ShowImage = true;
            // 
            // SetShotgunProject
            // 
            this.SetShotgunProject.Label = "Set Shotgun Project...";
            this.SetShotgunProject.Name = "SetShotgunProject";
            this.SetShotgunProject.OfficeImageId = "AllCategories";
            this.SetShotgunProject.ShowImage = true;
            this.SetShotgunProject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SetShotgunProject_Click);
            // 
            // PushToResourcesButton
            // 
            this.PushToResourcesButton.Description = "Update Resources from custom fields.";
            this.PushToResourcesButton.KeyTip = "PTR";
            this.PushToResourcesButton.Label = "Push to Resources";
            this.PushToResourcesButton.Name = "PushToResourcesButton";
            this.PushToResourcesButton.OfficeImageId = "ObjectsAlignLeft";
            this.PushToResourcesButton.ScreenTip = "Update Resources from custom fields.";
            this.PushToResourcesButton.ShowImage = true;
            this.PushToResourcesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PushToResourcesButton_Click);
            // 
            // UpdateDeliveries
            // 
            this.UpdateDeliveries.Description = "Update resource availabilities from the Delivery column.";
            this.UpdateDeliveries.KeyTip = "URA";
            this.UpdateDeliveries.Label = "Update Availabilities";
            this.UpdateDeliveries.Name = "UpdateDeliveries";
            this.UpdateDeliveries.OfficeImageId = "AccountMenu";
            this.UpdateDeliveries.ScreenTip = "Update resource availabilities from the Delivery column.";
            this.UpdateDeliveries.ShowImage = true;
            this.UpdateDeliveries.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.UpdateDeliveries_Click);
            // 
            // about
            // 
            this.about.Label = "About...";
            this.about.Name = "about";
            this.about.ShowImage = true;
            this.about.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.about_Click_1);
            // 
            // ShotgunRibbon
            // 
            this.Name = "ShotgunRibbon";
            this.RibbonType = "Microsoft.Project.Project";
            this.Tabs.Add(this.ShotgunTab);
            this.ShotgunTab.ResumeLayout(false);
            this.ShotgunTab.PerformLayout();
            this.ShotgunProjectPlugin.ResumeLayout(false);
            this.ShotgunProjectPlugin.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonGroup ShotgunProjectPlugin;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PushToResourcesButton;
        public Microsoft.Office.Tools.Ribbon.RibbonTab ShotgunTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton UpdateDeliveries;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu Advanced;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PushToShotgun;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PullFromShotgun;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SetShotgunProject;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton about;
    }

    partial class ThisRibbonCollection
    {
        internal ShotgunRibbon Ribbon1
        {
            get { return this.GetRibbon<ShotgunRibbon>(); }
        }
    }
}
