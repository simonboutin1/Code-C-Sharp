namespace Administration
{
    partial class frmJobGroupingEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJobGroupingEdit));
            this.dgvSelectedJobs = new System.Windows.Forms.DataGridView();
            this.dgvAvailableJobs = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveOneRight = new System.Windows.Forms.Button();
            this.btnMoveAllLeft = new System.Windows.Forms.Button();
            this.btnMoveOneLeft = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMoveAllRight = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.checkOperationManager = new System.Windows.Forms.CheckBox();
            this.checkEndOfJob = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableJobs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSelectedJobs
            // 
            this.dgvSelectedJobs.AllowUserToAddRows = false;
            this.dgvSelectedJobs.AllowUserToDeleteRows = false;
            this.dgvSelectedJobs.AllowUserToResizeRows = false;
            this.dgvSelectedJobs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvSelectedJobs, "dgvSelectedJobs");
            this.dgvSelectedJobs.MultiSelect = false;
            this.dgvSelectedJobs.Name = "dgvSelectedJobs";
            this.dgvSelectedJobs.ReadOnly = true;
            this.dgvSelectedJobs.RowHeadersVisible = false;
            this.dgvSelectedJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedJobs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSelectedJobs_CellFormatting);
            // 
            // dgvAvailableJobs
            // 
            this.dgvAvailableJobs.AllowUserToAddRows = false;
            this.dgvAvailableJobs.AllowUserToDeleteRows = false;
            this.dgvAvailableJobs.AllowUserToResizeRows = false;
            this.dgvAvailableJobs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvAvailableJobs, "dgvAvailableJobs");
            this.dgvAvailableJobs.MultiSelect = false;
            this.dgvAvailableJobs.Name = "dgvAvailableJobs";
            this.dgvAvailableJobs.ReadOnly = true;
            this.dgvAvailableJobs.RowHeadersVisible = false;
            this.dgvAvailableJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableJobs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAvailableJobs_CellFormatting);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMoveOneRight
            // 
            resources.ApplyResources(this.btnMoveOneRight, "btnMoveOneRight");
            this.btnMoveOneRight.Name = "btnMoveOneRight";
            this.btnMoveOneRight.UseVisualStyleBackColor = true;
            this.btnMoveOneRight.Click += new System.EventHandler(this.btnMoveOneRight_Click);
            // 
            // btnMoveAllLeft
            // 
            resources.ApplyResources(this.btnMoveAllLeft, "btnMoveAllLeft");
            this.btnMoveAllLeft.Name = "btnMoveAllLeft";
            this.btnMoveAllLeft.UseVisualStyleBackColor = true;
            this.btnMoveAllLeft.Click += new System.EventHandler(this.btnMoveAllLeft_Click);
            // 
            // btnMoveOneLeft
            // 
            resources.ApplyResources(this.btnMoveOneLeft, "btnMoveOneLeft");
            this.btnMoveOneLeft.Name = "btnMoveOneLeft";
            this.btnMoveOneLeft.UseVisualStyleBackColor = true;
            this.btnMoveOneLeft.Click += new System.EventHandler(this.btnMoveOneLeft_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvSelectedJobs);
            this.groupBox1.Controls.Add(this.dgvAvailableJobs);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnMoveAllLeft);
            this.groupBox1.Controls.Add(this.btnMoveAllRight);
            this.groupBox1.Controls.Add(this.btnMoveOneLeft);
            this.groupBox1.Controls.Add(this.btnMoveOneRight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnMoveAllRight
            // 
            resources.ApplyResources(this.btnMoveAllRight, "btnMoveAllRight");
            this.btnMoveAllRight.Name = "btnMoveAllRight";
            this.btnMoveAllRight.UseVisualStyleBackColor = true;
            this.btnMoveAllRight.Click += new System.EventHandler(this.btnMoveAllRight_Click);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // checkOperationManager
            // 
            resources.ApplyResources(this.checkOperationManager, "checkOperationManager");
            this.checkOperationManager.Name = "checkOperationManager";
            this.checkOperationManager.UseVisualStyleBackColor = true;
            // 
            // checkEndOfJob
            // 
            resources.ApplyResources(this.checkEndOfJob, "checkEndOfJob");
            this.checkEndOfJob.Name = "checkEndOfJob";
            this.checkEndOfJob.UseVisualStyleBackColor = true;
            // 
            // frmJobGroupingEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkEndOfJob);
            this.Controls.Add(this.checkOperationManager);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmJobGroupingEdit";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmJobGroupingEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmJobGroupingEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableJobs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSelectedJobs;
        private System.Windows.Forms.DataGridView dgvAvailableJobs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoveOneRight;
        private System.Windows.Forms.Button btnMoveAllLeft;
        private System.Windows.Forms.Button btnMoveOneLeft;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMoveAllRight;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox checkOperationManager;
        private System.Windows.Forms.CheckBox checkEndOfJob;
    }
}