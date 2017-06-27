namespace Administration
{
    partial class frmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmEndPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmSortPattern = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmWorkType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuJobGrouping = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmJobGrouping = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPermissions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmUserProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmAccessGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmSystemDate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmCalendars = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExchangeRate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmExchangeRate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmAboutAdministration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmAboutIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnExchangeRate = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCalendars = new System.Windows.Forms.Button();
            this.btnSystemDate = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAccessGroup = new System.Windows.Forms.Button();
            this.btnUserProfile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnJobGrouping = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWorkType = new System.Windows.Forms.Button();
            this.btnSortPattern = new System.Windows.Forms.Button();
            this.btnEndPoint = new System.Windows.Forms.Button();
            this.btnAddress = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuSort,
            this.mnuJobGrouping,
            this.mnuPermissions,
            this.mnuDate,
            this.mnuExchangeRate,
            this.mnuHelp});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmQuit});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // mnuItmQuit
            // 
            this.mnuItmQuit.Name = "mnuItmQuit";
            resources.ApplyResources(this.mnuItmQuit, "mnuItmQuit");
            this.mnuItmQuit.Click += new System.EventHandler(this.mnuItmQuit_Click);
            // 
            // mnuSort
            // 
            this.mnuSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmAddress,
            this.mnuItmEndPoint,
            this.mnuItmSortPattern,
            this.mnuItmWorkType});
            this.mnuSort.Name = "mnuSort";
            resources.ApplyResources(this.mnuSort, "mnuSort");
            // 
            // mnuItmAddress
            // 
            this.mnuItmAddress.Name = "mnuItmAddress";
            resources.ApplyResources(this.mnuItmAddress, "mnuItmAddress");
            this.mnuItmAddress.Click += new System.EventHandler(this.mnuItmAddress_Click);
            // 
            // mnuItmEndPoint
            // 
            this.mnuItmEndPoint.Name = "mnuItmEndPoint";
            resources.ApplyResources(this.mnuItmEndPoint, "mnuItmEndPoint");
            this.mnuItmEndPoint.Click += new System.EventHandler(this.mnuItmEndPoint_Click);
            // 
            // mnuItmSortPattern
            // 
            this.mnuItmSortPattern.Name = "mnuItmSortPattern";
            resources.ApplyResources(this.mnuItmSortPattern, "mnuItmSortPattern");
            this.mnuItmSortPattern.Click += new System.EventHandler(this.mnuItmSortPattern_Click);
            // 
            // mnuItmWorkType
            // 
            this.mnuItmWorkType.Name = "mnuItmWorkType";
            resources.ApplyResources(this.mnuItmWorkType, "mnuItmWorkType");
            this.mnuItmWorkType.Click += new System.EventHandler(this.mnuItmWorkType_Click);
            // 
            // mnuJobGrouping
            // 
            this.mnuJobGrouping.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmJobGrouping});
            this.mnuJobGrouping.Name = "mnuJobGrouping";
            resources.ApplyResources(this.mnuJobGrouping, "mnuJobGrouping");
            // 
            // mnuItmJobGrouping
            // 
            this.mnuItmJobGrouping.Name = "mnuItmJobGrouping";
            resources.ApplyResources(this.mnuItmJobGrouping, "mnuItmJobGrouping");
            this.mnuItmJobGrouping.Click += new System.EventHandler(this.mnuItmJobGrouping_Click);
            // 
            // mnuPermissions
            // 
            this.mnuPermissions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmUserProfile,
            this.mnuItmAccessGroup});
            this.mnuPermissions.Name = "mnuPermissions";
            resources.ApplyResources(this.mnuPermissions, "mnuPermissions");
            // 
            // mnuItmUserProfile
            // 
            this.mnuItmUserProfile.Name = "mnuItmUserProfile";
            resources.ApplyResources(this.mnuItmUserProfile, "mnuItmUserProfile");
            this.mnuItmUserProfile.Click += new System.EventHandler(this.mnuItmUserProfile_Click);
            // 
            // mnuItmAccessGroup
            // 
            this.mnuItmAccessGroup.Name = "mnuItmAccessGroup";
            resources.ApplyResources(this.mnuItmAccessGroup, "mnuItmAccessGroup");
            this.mnuItmAccessGroup.Click += new System.EventHandler(this.mnuItmAccessGroup_Click);
            // 
            // mnuDate
            // 
            this.mnuDate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmSystemDate,
            this.mnuItmCalendars});
            this.mnuDate.Name = "mnuDate";
            resources.ApplyResources(this.mnuDate, "mnuDate");
            // 
            // mnuItmSystemDate
            // 
            this.mnuItmSystemDate.Name = "mnuItmSystemDate";
            resources.ApplyResources(this.mnuItmSystemDate, "mnuItmSystemDate");
            this.mnuItmSystemDate.Click += new System.EventHandler(this.mnuItmSystemDate_Click);
            // 
            // mnuItmCalendars
            // 
            this.mnuItmCalendars.Name = "mnuItmCalendars";
            resources.ApplyResources(this.mnuItmCalendars, "mnuItmCalendars");
            this.mnuItmCalendars.Click += new System.EventHandler(this.mnuItmCalendars_Click);
            // 
            // mnuExchangeRate
            // 
            this.mnuExchangeRate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmExchangeRate});
            this.mnuExchangeRate.Name = "mnuExchangeRate";
            resources.ApplyResources(this.mnuExchangeRate, "mnuExchangeRate");
            // 
            // mnuItmExchangeRate
            // 
            this.mnuItmExchangeRate.Name = "mnuItmExchangeRate";
            resources.ApplyResources(this.mnuItmExchangeRate, "mnuItmExchangeRate");
            this.mnuItmExchangeRate.Click += new System.EventHandler(this.mnuItmExchangeRate_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmHelp,
            this.mnuItmAboutAdministration,
            this.mnuItmAboutIcons});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // mnuItmHelp
            // 
            this.mnuItmHelp.Name = "mnuItmHelp";
            resources.ApplyResources(this.mnuItmHelp, "mnuItmHelp");
            this.mnuItmHelp.Click += new System.EventHandler(this.mnuItmHelp_Click);
            // 
            // mnuItmAboutAdministration
            // 
            this.mnuItmAboutAdministration.Name = "mnuItmAboutAdministration";
            resources.ApplyResources(this.mnuItmAboutAdministration, "mnuItmAboutAdministration");
            this.mnuItmAboutAdministration.Click += new System.EventHandler(this.mnuItmAboutAdministration_Click);
            // 
            // mnuItmAboutIcons
            // 
            this.mnuItmAboutIcons.Name = "mnuItmAboutIcons";
            resources.ApplyResources(this.mnuItmAboutIcons, "mnuItmAboutIcons");
            this.mnuItmAboutIcons.Click += new System.EventHandler(this.mnuItmAboutIcons_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExchangeRate);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // btnExchangeRate
            // 
            resources.ApplyResources(this.btnExchangeRate, "btnExchangeRate");
            this.btnExchangeRate.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExchangeRate.Name = "btnExchangeRate";
            this.toolTip1.SetToolTip(this.btnExchangeRate, resources.GetString("btnExchangeRate.ToolTip"));
            this.btnExchangeRate.UseVisualStyleBackColor = true;
            this.btnExchangeRate.Click += new System.EventHandler(this.btnExchangeRate_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnCalendars);
            this.panel4.Controls.Add(this.btnSystemDate);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // btnCalendars
            // 
            resources.ApplyResources(this.btnCalendars, "btnCalendars");
            this.btnCalendars.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCalendars.Name = "btnCalendars";
            this.toolTip1.SetToolTip(this.btnCalendars, resources.GetString("btnCalendars.ToolTip"));
            this.btnCalendars.UseVisualStyleBackColor = true;
            this.btnCalendars.Click += new System.EventHandler(this.btnCalendars_Click);
            // 
            // btnSystemDate
            // 
            resources.ApplyResources(this.btnSystemDate, "btnSystemDate");
            this.btnSystemDate.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSystemDate.Name = "btnSystemDate";
            this.toolTip1.SetToolTip(this.btnSystemDate, resources.GetString("btnSystemDate.ToolTip"));
            this.btnSystemDate.UseVisualStyleBackColor = true;
            this.btnSystemDate.Click += new System.EventHandler(this.btnSystemDate_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnAccessGroup);
            this.panel3.Controls.Add(this.btnUserProfile);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // btnAccessGroup
            // 
            resources.ApplyResources(this.btnAccessGroup, "btnAccessGroup");
            this.btnAccessGroup.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAccessGroup.Name = "btnAccessGroup";
            this.toolTip1.SetToolTip(this.btnAccessGroup, resources.GetString("btnAccessGroup.ToolTip"));
            this.btnAccessGroup.UseVisualStyleBackColor = true;
            this.btnAccessGroup.Click += new System.EventHandler(this.btnAccessGroup_Click);
            // 
            // btnUserProfile
            // 
            resources.ApplyResources(this.btnUserProfile, "btnUserProfile");
            this.btnUserProfile.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUserProfile.Name = "btnUserProfile";
            this.toolTip1.SetToolTip(this.btnUserProfile, resources.GetString("btnUserProfile.ToolTip"));
            this.btnUserProfile.UseVisualStyleBackColor = true;
            this.btnUserProfile.Click += new System.EventHandler(this.btnUserProfile_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnJobGrouping);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // btnJobGrouping
            // 
            resources.ApplyResources(this.btnJobGrouping, "btnJobGrouping");
            this.btnJobGrouping.ForeColor = System.Drawing.SystemColors.Control;
            this.btnJobGrouping.Name = "btnJobGrouping";
            this.toolTip1.SetToolTip(this.btnJobGrouping, resources.GetString("btnJobGrouping.ToolTip"));
            this.btnJobGrouping.UseVisualStyleBackColor = true;
            this.btnJobGrouping.Click += new System.EventHandler(this.btnJobGrouping_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnWorkType);
            this.panel1.Controls.Add(this.btnSortPattern);
            this.panel1.Controls.Add(this.btnEndPoint);
            this.panel1.Controls.Add(this.btnAddress);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnWorkType
            // 
            resources.ApplyResources(this.btnWorkType, "btnWorkType");
            this.btnWorkType.ForeColor = System.Drawing.SystemColors.Control;
            this.btnWorkType.Name = "btnWorkType";
            this.toolTip1.SetToolTip(this.btnWorkType, resources.GetString("btnWorkType.ToolTip"));
            this.btnWorkType.UseVisualStyleBackColor = true;
            this.btnWorkType.Click += new System.EventHandler(this.btnWorkType_Click);
            // 
            // btnSortPattern
            // 
            resources.ApplyResources(this.btnSortPattern, "btnSortPattern");
            this.btnSortPattern.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSortPattern.Name = "btnSortPattern";
            this.toolTip1.SetToolTip(this.btnSortPattern, resources.GetString("btnSortPattern.ToolTip"));
            this.btnSortPattern.UseVisualStyleBackColor = true;
            this.btnSortPattern.Click += new System.EventHandler(this.btnSortPattern_Click);
            // 
            // btnEndPoint
            // 
            resources.ApplyResources(this.btnEndPoint, "btnEndPoint");
            this.btnEndPoint.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEndPoint.Name = "btnEndPoint";
            this.toolTip1.SetToolTip(this.btnEndPoint, resources.GetString("btnEndPoint.ToolTip"));
            this.btnEndPoint.UseVisualStyleBackColor = true;
            this.btnEndPoint.Click += new System.EventHandler(this.btnEndPoint_Click);
            // 
            // btnAddress
            // 
            resources.ApplyResources(this.btnAddress, "btnAddress");
            this.btnAddress.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddress.Name = "btnAddress";
            this.toolTip1.SetToolTip(this.btnAddress, resources.GetString("btnAddress.ToolTip"));
            this.btnAddress.UseVisualStyleBackColor = true;
            this.btnAddress.Click += new System.EventHandler(this.btnAddress_Click);
            // 
            // frmMainMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMainMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdministration_FormClosing);
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.frmAdministration_HelpRequested);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuItmQuit;
        private System.Windows.Forms.ToolStripMenuItem mnuSort;
        private System.Windows.Forms.ToolStripMenuItem mnuItmAddress;
        private System.Windows.Forms.ToolStripMenuItem mnuItmEndPoint;
        private System.Windows.Forms.ToolStripMenuItem mnuItmSortPattern;
        private System.Windows.Forms.ToolStripMenuItem mnuItmWorkType;
        private System.Windows.Forms.ToolStripMenuItem mnuJobGrouping;
        private System.Windows.Forms.ToolStripMenuItem mnuItmJobGrouping;
        private System.Windows.Forms.ToolStripMenuItem mnuPermissions;
        private System.Windows.Forms.ToolStripMenuItem mnuItmUserProfile;
        private System.Windows.Forms.ToolStripMenuItem mnuItmAccessGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuDate;
        private System.Windows.Forms.ToolStripMenuItem mnuItmSystemDate;
        private System.Windows.Forms.ToolStripMenuItem mnuItmCalendars;
        private System.Windows.Forms.ToolStripMenuItem mnuExchangeRate;
        private System.Windows.Forms.ToolStripMenuItem mnuItmExchangeRate;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuItmHelp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExchangeRate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCalendars;
        private System.Windows.Forms.Button btnSystemDate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnAccessGroup;
        private System.Windows.Forms.Button btnUserProfile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnJobGrouping;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnWorkType;
        private System.Windows.Forms.Button btnSortPattern;
        private System.Windows.Forms.Button btnEndPoint;
        private System.Windows.Forms.Button btnAddress;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem mnuItmAboutAdministration;
        private System.Windows.Forms.ToolStripMenuItem mnuItmAboutIcons;
    }
}