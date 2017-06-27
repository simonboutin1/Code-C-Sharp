//2012-06 Simon Boutin MANTIS# 16396 : Convert LBTables from VB to C#
//using BancTec.PCR2P.Core.DatabaseModel.Login;

namespace Administration
{
    partial class frmExchangeRateEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExchangeRateEdit));
            this.dtpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.grpBoxIdentification = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCurrencyType2 = new System.Windows.Forms.ComboBox();
            this.cboCurrencyType1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvRate = new System.Windows.Forms.DataGridView();
            this.grpBoxIdentification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRate)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpEffectiveDate
            // 
            resources.ApplyResources(this.dtpEffectiveDate, "dtpEffectiveDate");
            this.dtpEffectiveDate.Name = "dtpEffectiveDate";
            this.dtpEffectiveDate.ValueChanged += new System.EventHandler(this.dtpEffectiveDate_ValueChanged);
            // 
            // grpBoxIdentification
            // 
            this.grpBoxIdentification.Controls.Add(this.label3);
            this.grpBoxIdentification.Controls.Add(this.cboCurrencyType2);
            this.grpBoxIdentification.Controls.Add(this.cboCurrencyType1);
            this.grpBoxIdentification.Controls.Add(this.label1);
            this.grpBoxIdentification.Controls.Add(this.label2);
            this.grpBoxIdentification.Controls.Add(this.dtpEffectiveDate);
            this.grpBoxIdentification.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.grpBoxIdentification, "grpBoxIdentification");
            this.grpBoxIdentification.Name = "grpBoxIdentification";
            this.grpBoxIdentification.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboCurrencyType2
            // 
            this.cboCurrencyType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrencyType2.FormattingEnabled = true;
            resources.ApplyResources(this.cboCurrencyType2, "cboCurrencyType2");
            this.cboCurrencyType2.Name = "cboCurrencyType2";
            this.cboCurrencyType2.SelectedValueChanged += new System.EventHandler(this.cboCurrencyType2_SelectedValueChanged);
            // 
            // cboCurrencyType1
            // 
            this.cboCurrencyType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrencyType1.FormattingEnabled = true;
            resources.ApplyResources(this.cboCurrencyType1, "cboCurrencyType1");
            this.cboCurrencyType1.Name = "cboCurrencyType1";
            this.cboCurrencyType1.SelectedValueChanged += new System.EventHandler(this.cboCurrencyType1_SelectedValueChanged);
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
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dgvRate
            // 
            this.dgvRate.AllowUserToResizeRows = false;
            this.dgvRate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvRate, "dgvRate");
            this.dgvRate.MultiSelect = false;
            this.dgvRate.Name = "dgvRate";
            this.dgvRate.RowHeadersVisible = false;
            this.dgvRate.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRate_CellValidating);
            this.dgvRate.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRate_CellValueChanged);
            this.dgvRate.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvRate_EditingControlShowing);
            // 
            // frmExchangeRateEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpBoxIdentification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExchangeRateEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUpdateExchangeRate_FormClosing);
            this.Load += new System.EventHandler(this.frmUpdateExchangeRate_Load);
            this.grpBoxIdentification.ResumeLayout(false);
            this.grpBoxIdentification.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpEffectiveDate;
        private System.Windows.Forms.GroupBox grpBoxIdentification;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCurrencyType2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboCurrencyType1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dgvRate;
    }
}