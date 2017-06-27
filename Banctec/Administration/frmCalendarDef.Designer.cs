namespace Administration
{
    partial class frmCalendarDef
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalendarDef));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabCalendar = new System.Windows.Forms.TabControl();
            this.tabSite = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSite = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSiteAddYear = new System.Windows.Forms.Button();
            this.cboSiteYear = new System.Windows.Forms.ComboBox();
            this.cboSiteMonth = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSiteClose = new System.Windows.Forms.Button();
            this.btnSiteSave = new System.Windows.Forms.Button();
            this.btnSiteCancel = new System.Windows.Forms.Button();
            this.tabJob = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cboJob = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnJobAddYear = new System.Windows.Forms.Button();
            this.cboJobYear = new System.Windows.Forms.ComboBox();
            this.cboJobMonth = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnJobClose = new System.Windows.Forms.Button();
            this.btnJobSave = new System.Windows.Forms.Button();
            this.btnJobCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnJobJobs = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnJobCopy = new System.Windows.Forms.Button();
            this.btnJobRemove = new System.Windows.Forms.Button();
            this.btnJobAdd = new System.Windows.Forms.Button();
            this.btnJobEdit = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCalendar.SuspendLayout();
            this.tabSite.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabJob.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tabCalendar, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tabCalendar
            // 
            this.tabCalendar.Controls.Add(this.tabSite);
            this.tabCalendar.Controls.Add(this.tabJob);
            resources.ApplyResources(this.tabCalendar, "tabCalendar");
            this.tabCalendar.Name = "tabCalendar";
            this.tabCalendar.SelectedIndex = 0;
            this.tabCalendar.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabCalendar_Selecting);
            this.tabCalendar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabCalendar_KeyDown);
            // 
            // tabSite
            // 
            this.tabSite.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.tabSite, "tabSite");
            this.tabSite.Name = "tabSite";
            this.tabSite.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cboSite, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboSite
            // 
            resources.ApplyResources(this.cboSite, "cboSite");
            this.cboSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSite.FormattingEnabled = true;
            this.cboSite.Name = "cboSite";
            this.cboSite.SelectedValueChanged += new System.EventHandler(this.cboSite_SelectedValueChanged);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSiteAddYear, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.cboSiteYear, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.cboSiteMonth, 5, 1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnSiteAddYear
            // 
            resources.ApplyResources(this.btnSiteAddYear, "btnSiteAddYear");
            this.btnSiteAddYear.Name = "btnSiteAddYear";
            this.btnSiteAddYear.UseVisualStyleBackColor = true;
            this.btnSiteAddYear.Click += new System.EventHandler(this.btnSiteAddYear_Click);
            // 
            // cboSiteYear
            // 
            resources.ApplyResources(this.cboSiteYear, "cboSiteYear");
            this.cboSiteYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSiteYear.FormattingEnabled = true;
            this.cboSiteYear.Name = "cboSiteYear";
            this.cboSiteYear.SelectedValueChanged += new System.EventHandler(this.cboSiteYear_SelectedValueChanged);
            // 
            // cboSiteMonth
            // 
            resources.ApplyResources(this.cboSiteMonth, "cboSiteMonth");
            this.cboSiteMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSiteMonth.FormattingEnabled = true;
            this.cboSiteMonth.Name = "cboSiteMonth";
            this.cboSiteMonth.SelectedValueChanged += new System.EventHandler(this.cboSiteMonth_SelectedValueChanged);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSiteClose);
            this.panel1.Controls.Add(this.btnSiteSave);
            this.panel1.Controls.Add(this.btnSiteCancel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnSiteClose
            // 
            resources.ApplyResources(this.btnSiteClose, "btnSiteClose");
            this.btnSiteClose.Name = "btnSiteClose";
            this.btnSiteClose.UseVisualStyleBackColor = true;
            this.btnSiteClose.Click += new System.EventHandler(this.btnSiteClose_Click);
            // 
            // btnSiteSave
            // 
            resources.ApplyResources(this.btnSiteSave, "btnSiteSave");
            this.btnSiteSave.Name = "btnSiteSave";
            this.btnSiteSave.UseVisualStyleBackColor = true;
            this.btnSiteSave.Click += new System.EventHandler(this.btnSiteSave_Click);
            // 
            // btnSiteCancel
            // 
            resources.ApplyResources(this.btnSiteCancel, "btnSiteCancel");
            this.btnSiteCancel.Name = "btnSiteCancel";
            this.btnSiteCancel.UseVisualStyleBackColor = true;
            this.btnSiteCancel.Click += new System.EventHandler(this.btnSiteCancel_Click);
            // 
            // tabJob
            // 
            this.tabJob.Controls.Add(this.tableLayoutPanel6);
            resources.ApplyResources(this.tabJob, "tabJob");
            this.tabJob.Name = "tabJob";
            this.tabJob.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel9, 1, 1);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.cboJob, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cboJob
            // 
            resources.ApplyResources(this.cboJob, "cboJob");
            this.cboJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJob.FormattingEnabled = true;
            this.cboJob.Name = "cboJob";
            this.cboJob.SelectedValueChanged += new System.EventHandler(this.cboJob_SelectedValueChanged);
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.label6, 5, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnJobAddYear, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.cboJobYear, 3, 1);
            this.tableLayoutPanel8.Controls.Add(this.cboJobMonth, 5, 1);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnJobAddYear
            // 
            resources.ApplyResources(this.btnJobAddYear, "btnJobAddYear");
            this.btnJobAddYear.Name = "btnJobAddYear";
            this.btnJobAddYear.UseVisualStyleBackColor = true;
            this.btnJobAddYear.Click += new System.EventHandler(this.btnJobAddYear_Click);
            // 
            // cboJobYear
            // 
            resources.ApplyResources(this.cboJobYear, "cboJobYear");
            this.cboJobYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJobYear.FormattingEnabled = true;
            this.cboJobYear.Name = "cboJobYear";
            // 
            // cboJobMonth
            // 
            resources.ApplyResources(this.cboJobMonth, "cboJobMonth");
            this.cboJobMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJobMonth.FormattingEnabled = true;
            this.cboJobMonth.Name = "cboJobMonth";
            this.cboJobMonth.SelectedValueChanged += new System.EventHandler(this.cboJobMonth_SelectedValueChanged);
            // 
            // tableLayoutPanel9
            // 
            resources.ApplyResources(this.tableLayoutPanel9, "tableLayoutPanel9");
            this.tableLayoutPanel9.Controls.Add(this.panel2, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnJobClose);
            this.panel2.Controls.Add(this.btnJobSave);
            this.panel2.Controls.Add(this.btnJobCancel);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // btnJobClose
            // 
            resources.ApplyResources(this.btnJobClose, "btnJobClose");
            this.btnJobClose.Name = "btnJobClose";
            this.btnJobClose.UseVisualStyleBackColor = true;
            this.btnJobClose.Click += new System.EventHandler(this.btnJobClose_Click);
            // 
            // btnJobSave
            // 
            resources.ApplyResources(this.btnJobSave, "btnJobSave");
            this.btnJobSave.Name = "btnJobSave";
            this.btnJobSave.UseVisualStyleBackColor = true;
            this.btnJobSave.Click += new System.EventHandler(this.btnJobSave_Click);
            // 
            // btnJobCancel
            // 
            resources.ApplyResources(this.btnJobCancel, "btnJobCancel");
            this.btnJobCancel.Name = "btnJobCancel";
            this.btnJobCancel.UseVisualStyleBackColor = true;
            this.btnJobCancel.Click += new System.EventHandler(this.btnJobCancel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnJobJobs);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // btnJobJobs
            // 
            resources.ApplyResources(this.btnJobJobs, "btnJobJobs");
            this.btnJobJobs.Name = "btnJobJobs";
            this.btnJobJobs.UseVisualStyleBackColor = true;
            this.btnJobJobs.Click += new System.EventHandler(this.btnJobJobs_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnJobCopy);
            this.panel4.Controls.Add(this.btnJobRemove);
            this.panel4.Controls.Add(this.btnJobAdd);
            this.panel4.Controls.Add(this.btnJobEdit);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // btnJobCopy
            // 
            resources.ApplyResources(this.btnJobCopy, "btnJobCopy");
            this.btnJobCopy.Name = "btnJobCopy";
            this.btnJobCopy.UseVisualStyleBackColor = true;
            this.btnJobCopy.Click += new System.EventHandler(this.btnJobCopy_Click);
            // 
            // btnJobRemove
            // 
            resources.ApplyResources(this.btnJobRemove, "btnJobRemove");
            this.btnJobRemove.Name = "btnJobRemove";
            this.btnJobRemove.UseVisualStyleBackColor = true;
            this.btnJobRemove.Click += new System.EventHandler(this.btnJobRemove_Click);
            // 
            // btnJobAdd
            // 
            resources.ApplyResources(this.btnJobAdd, "btnJobAdd");
            this.btnJobAdd.Name = "btnJobAdd";
            this.btnJobAdd.UseVisualStyleBackColor = true;
            this.btnJobAdd.Click += new System.EventHandler(this.btnJobAdd_Click);
            // 
            // btnJobEdit
            // 
            resources.ApplyResources(this.btnJobEdit, "btnJobEdit");
            this.btnJobEdit.Name = "btnJobEdit";
            this.btnJobEdit.UseVisualStyleBackColor = true;
            this.btnJobEdit.Click += new System.EventHandler(this.btnJobEdit_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // frmCalendarDef
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmCalendarDef";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalendar_FormClosing);
            this.Load += new System.EventHandler(this.frmCalendar_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabCalendar.ResumeLayout(false);
            this.tabSite.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabJob.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabCalendar;
        private System.Windows.Forms.TabPage tabSite;
        private System.Windows.Forms.TabPage tabJob;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSite;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox cboSiteMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSiteAddYear;
        private System.Windows.Forms.ComboBox cboSiteYear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSiteClose;
        private System.Windows.Forms.Button btnSiteSave;
        private System.Windows.Forms.Button btnSiteCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboJob;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnJobAddYear;
        private System.Windows.Forms.ComboBox cboJobYear;
        private System.Windows.Forms.ComboBox cboJobMonth;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnJobClose;
        private System.Windows.Forms.Button btnJobSave;
        private System.Windows.Forms.Button btnJobCancel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnJobJobs;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnJobCopy;
        private System.Windows.Forms.Button btnJobRemove;
        private System.Windows.Forms.Button btnJobAdd;
        private System.Windows.Forms.Button btnJobEdit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}