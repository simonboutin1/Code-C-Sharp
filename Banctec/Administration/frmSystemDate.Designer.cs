namespace Administration
{
    partial class frmSystemDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSystemDate));
            this.grpProcessingDate = new System.Windows.Forms.GroupBox();
            this.dtpSystemDate = new System.Windows.Forms.DateTimePicker();
            this.radioFollowing = new System.Windows.Forms.RadioButton();
            this.radioDefault = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpProcessingDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProcessingDate
            // 
            resources.ApplyResources(this.grpProcessingDate, "grpProcessingDate");
            this.grpProcessingDate.Controls.Add(this.dtpSystemDate);
            this.grpProcessingDate.Controls.Add(this.radioFollowing);
            this.grpProcessingDate.Controls.Add(this.radioDefault);
            this.grpProcessingDate.ForeColor = System.Drawing.Color.Black;
            this.grpProcessingDate.Name = "grpProcessingDate";
            this.grpProcessingDate.TabStop = false;
            // 
            // dtpSystemDate
            // 
            resources.ApplyResources(this.dtpSystemDate, "dtpSystemDate");
            this.dtpSystemDate.Name = "dtpSystemDate";
            // 
            // radioFollowing
            // 
            resources.ApplyResources(this.radioFollowing, "radioFollowing");
            this.radioFollowing.Name = "radioFollowing";
            this.radioFollowing.TabStop = true;
            this.radioFollowing.UseVisualStyleBackColor = true;
            // 
            // radioDefault
            // 
            resources.ApplyResources(this.radioDefault, "radioDefault");
            this.radioDefault.Name = "radioDefault";
            this.radioDefault.TabStop = true;
            this.radioDefault.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSystemDate
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpProcessingDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSystemDate";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSystemDate_FormClosing);
            this.Load += new System.EventHandler(this.frmSystemDate_Load);
            this.grpProcessingDate.ResumeLayout(false);
            this.grpProcessingDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProcessingDate;
        private System.Windows.Forms.RadioButton radioFollowing;
        private System.Windows.Forms.RadioButton radioDefault;
        private System.Windows.Forms.DateTimePicker dtpSystemDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}