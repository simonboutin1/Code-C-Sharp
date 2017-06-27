namespace Administration
{
    partial class frmJobGroupingDef
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJobGroupingDef));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRemoveJobGroups = new System.Windows.Forms.Button();
            this.btnEditJobGroups = new System.Windows.Forms.Button();
            this.btnAddJobGroups = new System.Windows.Forms.Button();
            this.dgvJobGroups = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grpBoxJobs = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvJobs = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobGroups)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.grpBoxJobs.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.dgvJobGroups, 1, 1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRemoveJobGroups);
            this.panel1.Controls.Add(this.btnEditJobGroups);
            this.panel1.Controls.Add(this.btnAddJobGroups);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnRemoveJobGroups
            // 
            resources.ApplyResources(this.btnRemoveJobGroups, "btnRemoveJobGroups");
            this.btnRemoveJobGroups.Name = "btnRemoveJobGroups";
            this.btnRemoveJobGroups.UseVisualStyleBackColor = true;
            this.btnRemoveJobGroups.Click += new System.EventHandler(this.btnRemoveJobGroups_Click);
            // 
            // btnEditJobGroups
            // 
            resources.ApplyResources(this.btnEditJobGroups, "btnEditJobGroups");
            this.btnEditJobGroups.Name = "btnEditJobGroups";
            this.btnEditJobGroups.UseVisualStyleBackColor = true;
            this.btnEditJobGroups.Click += new System.EventHandler(this.btnEditJobGroups_Click);
            // 
            // btnAddJobGroups
            // 
            resources.ApplyResources(this.btnAddJobGroups, "btnAddJobGroups");
            this.btnAddJobGroups.Name = "btnAddJobGroups";
            this.btnAddJobGroups.UseVisualStyleBackColor = true;
            this.btnAddJobGroups.Click += new System.EventHandler(this.btnAddJobGroups_Click);
            // 
            // dgvJobGroups
            // 
            this.dgvJobGroups.AllowUserToAddRows = false;
            this.dgvJobGroups.AllowUserToDeleteRows = false;
            this.dgvJobGroups.AllowUserToResizeRows = false;
            this.dgvJobGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJobGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvJobGroups, "dgvJobGroups");
            this.dgvJobGroups.MultiSelect = false;
            this.dgvJobGroups.Name = "dgvJobGroups";
            this.dgvJobGroups.ReadOnly = true;
            this.dgvJobGroups.RowHeadersVisible = false;
            this.dgvJobGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJobGroups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobGroups_CellDoubleClick);
            this.dgvJobGroups.CurrentCellChanged += new System.EventHandler(this.dgvJobGroups_CurrentCellChanged);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.grpBoxJobs, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // grpBoxJobs
            // 
            this.grpBoxJobs.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.grpBoxJobs, "grpBoxJobs");
            this.grpBoxJobs.ForeColor = System.Drawing.Color.Black;
            this.grpBoxJobs.Name = "grpBoxJobs";
            this.grpBoxJobs.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.dgvJobs, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // dgvJobs
            // 
            this.dgvJobs.AllowUserToAddRows = false;
            this.dgvJobs.AllowUserToDeleteRows = false;
            this.dgvJobs.AllowUserToResizeRows = false;
            this.dgvJobs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvJobs, "dgvJobs");
            this.dgvJobs.MultiSelect = false;
            this.dgvJobs.Name = "dgvJobs";
            this.dgvJobs.ReadOnly = true;
            this.dgvJobs.RowHeadersVisible = false;
            this.dgvJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJobs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvJobs_CellFormatting);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnClose);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmJobGroupingDef
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmJobGroupingDef";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmJobGroupingDef_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobGroups)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.grpBoxJobs.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRemoveJobGroups;
        private System.Windows.Forms.Button btnEditJobGroups;
        private System.Windows.Forms.Button btnAddJobGroups;
        public System.Windows.Forms.DataGridView dgvJobGroups;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox grpBoxJobs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.DataGridView dgvJobs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}