namespace Administration
{
    partial class frmAboutIcons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAboutIcons));
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.linkFatCow = new System.Windows.Forms.LinkLabel();
            this.linkCreativeCommons = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // linkFatCow
            // 
            resources.ApplyResources(this.linkFatCow, "linkFatCow");
            this.linkFatCow.BackColor = System.Drawing.Color.Transparent;
            this.linkFatCow.Name = "linkFatCow";
            this.linkFatCow.TabStop = true;
            this.linkFatCow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFatCow_LinkClicked);
            // 
            // linkCreativeCommons
            // 
            resources.ApplyResources(this.linkCreativeCommons, "linkCreativeCommons");
            this.linkCreativeCommons.BackColor = System.Drawing.Color.Transparent;
            this.linkCreativeCommons.Name = "linkCreativeCommons";
            this.linkCreativeCommons.TabStop = true;
            this.linkCreativeCommons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCreativeCommons_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // frmAboutIcons
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkCreativeCommons);
            this.Controls.Add(this.linkFatCow);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAboutIcons";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmAboutIcons_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel linkFatCow;
        private System.Windows.Forms.LinkLabel linkCreativeCommons;
        private System.Windows.Forms.Label label2;
    }
}