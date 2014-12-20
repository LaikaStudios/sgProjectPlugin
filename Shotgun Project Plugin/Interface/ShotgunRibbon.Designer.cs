namespace sg_prj
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
            this.Shotgun = this.Factory.CreateRibbonGroup();
            this.PushToResourcesButton = this.Factory.CreateRibbonButton();
            this.UpdateDeliveries = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.PushToShotgun = this.Factory.CreateRibbonButton();
            this.PullFromShotgun = this.Factory.CreateRibbonButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.Advanced = this.Factory.CreateRibbonMenu();
            this.PullFromResources = this.Factory.CreateRibbonButton();
            this.ShotgunTab.SuspendLayout();
            this.Shotgun.SuspendLayout();
            // 
            // ShotgunTab
            // 
            this.ShotgunTab.Groups.Add(this.Shotgun);
            this.ShotgunTab.KeyTip = "SG";
            this.ShotgunTab.Label = "Shotgun";
            this.ShotgunTab.Name = "ShotgunTab";
            // 
            // Shotgun
            // 
            this.Shotgun.Items.Add(this.PushToResourcesButton);
            this.Shotgun.Items.Add(this.UpdateDeliveries);
            this.Shotgun.Items.Add(this.separator1);
            this.Shotgun.Items.Add(this.PushToShotgun);
            this.Shotgun.Items.Add(this.PullFromShotgun);
            this.Shotgun.Items.Add(this.separator2);
            this.Shotgun.Items.Add(this.Advanced);
            this.Shotgun.Label = "Shotgun";
            this.Shotgun.Name = "Shotgun";
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
            this.PushToResourcesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.pushToResourcesButton_Click);
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
            // separator1
            // 
            this.separator1.Name = "separator1";
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
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // Advanced
            // 
            this.Advanced.Items.Add(this.PullFromResources);
            this.Advanced.Label = "Advanced";
            this.Advanced.Name = "Advanced";
            this.Advanced.OfficeImageId = "AccessFormWizard";
            this.Advanced.ShowImage = true;
            // 
            // PullFromResources
            // 
            this.PullFromResources.Description = "Pull custom field values from Resources.";
            this.PullFromResources.KeyTip = "PFR";
            this.PullFromResources.Label = "Pull From Resources";
            this.PullFromResources.Name = "PullFromResources";
            this.PullFromResources.OfficeImageId = "ParagraphSpacing";
            this.PullFromResources.ScreenTip = "Pull custom field values from Resources.";
            this.PullFromResources.ShowImage = true;
            this.PullFromResources.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PullFromResources_Click);
            // 
            // ShotgunRibbon
            // 
            this.Name = "ShotgunRibbon";
            this.RibbonType = "Microsoft.Project.Project";
            this.Tabs.Add(this.ShotgunTab);
            this.ShotgunTab.ResumeLayout(false);
            this.ShotgunTab.PerformLayout();
            this.Shotgun.ResumeLayout(false);
            this.Shotgun.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Shotgun;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PushToResourcesButton;
        public Microsoft.Office.Tools.Ribbon.RibbonTab ShotgunTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton UpdateDeliveries;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu Advanced;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PullFromResources;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PushToShotgun;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PullFromShotgun;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
    }

    partial class ThisRibbonCollection
    {
        internal ShotgunRibbon Ribbon1
        {
            get { return this.GetRibbon<ShotgunRibbon>(); }
        }
    }
}
