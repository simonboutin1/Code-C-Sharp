namespace Administration
{
    partial class frmDupDetection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDupDetection));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkActivate = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvRTs = new System.Windows.Forms.DataGridView();
            this.cboDupMode = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdAddRT = new System.Windows.Forms.Button();
            this.cmdRemoveRT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRT = new System.Windows.Forms.TextBox();
            this.txtBankAcct = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDaysLookBack = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.lstDocIDs = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdAddDocID = new System.Windows.Forms.Button();
            this.cmdRemoveDocID = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDocID = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.txtRejectReason = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRTs)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkActivate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            // 
            // chkActivate
            // 
            resources.ApplyResources(this.chkActivate, "chkActivate");
            this.chkActivate.Name = "chkActivate";
            this.chkActivate.UseVisualStyleBackColor = true;
            this.chkActivate.CheckedChanged += new System.EventHandler(this.chkActivate_CheckedChanged);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer2.Panel2.Controls.Add(this.cmdSave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cboDupMode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtDaysLookBack, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dgvRTs, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dgvRTs
            // 
            this.dgvRTs.AllowUserToAddRows = false;
            this.dgvRTs.AllowUserToDeleteRows = false;
            this.dgvRTs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvRTs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvRTs, "dgvRTs");
            this.dgvRTs.Name = "dgvRTs";
            this.dgvRTs.ReadOnly = true;
            this.dgvRTs.RowHeadersVisible = false;
            this.dgvRTs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRTs.ShowCellErrors = false;
            this.dgvRTs.ShowCellToolTips = false;
            this.dgvRTs.ShowEditingIcon = false;
            this.dgvRTs.ShowRowErrors = false;
            // 
            // cboDupMode
            // 
            resources.ApplyResources(this.cboDupMode, "cboDupMode");
            this.cboDupMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDupMode.FormattingEnabled = true;
            this.cboDupMode.Name = "cboDupMode";
            this.cboDupMode.SelectedIndexChanged += new System.EventHandler(this.cboDupMode_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.cmdAddRT, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmdRemoveRT, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.txtRT, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.txtBankAcct, 1, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // cmdAddRT
            // 
            resources.ApplyResources(this.cmdAddRT, "cmdAddRT");
            this.cmdAddRT.Name = "cmdAddRT";
            this.cmdAddRT.UseVisualStyleBackColor = true;
            this.cmdAddRT.Click += new System.EventHandler(this.cmdAddRT_Click);
            // 
            // cmdRemoveRT
            // 
            resources.ApplyResources(this.cmdRemoveRT, "cmdRemoveRT");
            this.cmdRemoveRT.Name = "cmdRemoveRT";
            this.cmdRemoveRT.UseVisualStyleBackColor = true;
            this.cmdRemoveRT.Click += new System.EventHandler(this.cmdRemoveRT_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtRT
            // 
            resources.ApplyResources(this.txtRT, "txtRT");
            this.txtRT.Name = "txtRT";
            // 
            // txtBankAcct
            // 
            resources.ApplyResources(this.txtBankAcct, "txtBankAcct");
            this.txtBankAcct.Name = "txtBankAcct";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtDaysLookBack
            // 
            resources.ApplyResources(this.txtDaysLookBack, "txtDaysLookBack");
            this.txtDaysLookBack.Name = "txtDaysLookBack";
            this.txtDaysLookBack.Validating += new System.ComponentModel.CancelEventHandler(this.NumericValHandler);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cboStatus, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtRejectReason, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lstDocIDs, 0, 1);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lstDocIDs
            // 
            resources.ApplyResources(this.lstDocIDs, "lstDocIDs");
            this.lstDocIDs.FormattingEnabled = true;
            this.lstDocIDs.Name = "lstDocIDs";
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.cmdAddDocID, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cmdRemoveDocID, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.cboDocID, 0, 2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // cmdAddDocID
            // 
            resources.ApplyResources(this.cmdAddDocID, "cmdAddDocID");
            this.cmdAddDocID.Name = "cmdAddDocID";
            this.cmdAddDocID.UseVisualStyleBackColor = true;
            this.cmdAddDocID.Click += new System.EventHandler(this.cmdAddDocID_Click);
            // 
            // cmdRemoveDocID
            // 
            resources.ApplyResources(this.cmdRemoveDocID, "cmdRemoveDocID");
            this.cmdRemoveDocID.Name = "cmdRemoveDocID";
            this.cmdRemoveDocID.UseVisualStyleBackColor = true;
            this.cmdRemoveDocID.Click += new System.EventHandler(this.cmdRemoveDocID_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cboDocID
            // 
            resources.ApplyResources(this.cboDocID, "cboDocID");
            this.cboDocID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanel7.SetColumnSpan(this.cboDocID, 2);
            this.cboDocID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocID.FormattingEnabled = true;
            this.cboDocID.Name = "cboDocID";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // cboStatus
            // 
            resources.ApplyResources(this.cboStatus, "cboStatus");
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Name = "cboStatus";
            // 
            // txtRejectReason
            // 
            resources.ApplyResources(this.txtRejectReason, "txtRejectReason");
            this.txtRejectReason.Name = "txtRejectReason";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // frmDupDetection
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmDupDetection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRTs)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.CheckBox chkActivate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDupMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button cmdAddRT;
        private System.Windows.Forms.Button cmdRemoveRT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDaysLookBack;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lstDocIDs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button cmdAddDocID;
        private System.Windows.Forms.Button cmdRemoveDocID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvRTs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRT;
        private System.Windows.Forms.TextBox txtBankAcct;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.ComboBox cboDocID;
        private System.Windows.Forms.TextBox txtRejectReason;
        private System.Windows.Forms.Label label8;

    }
}