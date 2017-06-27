namespace Administration
{
    partial class frmAddressEditAddress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddressEditAddress));
            this.grpBoxDetails = new System.Windows.Forms.GroupBox();
            this.labelAddressID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLine2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLine1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpBoxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxDetails
            // 
            resources.ApplyResources(this.grpBoxDetails, "grpBoxDetails");
            this.grpBoxDetails.Controls.Add(this.labelAddressID);
            this.grpBoxDetails.Controls.Add(this.label7);
            this.grpBoxDetails.Controls.Add(this.txtZipCode);
            this.grpBoxDetails.Controls.Add(this.label6);
            this.grpBoxDetails.Controls.Add(this.txtState);
            this.grpBoxDetails.Controls.Add(this.label5);
            this.grpBoxDetails.Controls.Add(this.txtCity);
            this.grpBoxDetails.Controls.Add(this.label4);
            this.grpBoxDetails.Controls.Add(this.txtLine2);
            this.grpBoxDetails.Controls.Add(this.label3);
            this.grpBoxDetails.Controls.Add(this.txtLine1);
            this.grpBoxDetails.Controls.Add(this.label2);
            this.grpBoxDetails.Controls.Add(this.txtName);
            this.grpBoxDetails.Controls.Add(this.label1);
            this.grpBoxDetails.ForeColor = System.Drawing.Color.Black;
            this.grpBoxDetails.Name = "grpBoxDetails";
            this.grpBoxDetails.TabStop = false;
            // 
            // labelAddressID
            // 
            resources.ApplyResources(this.labelAddressID, "labelAddressID");
            this.labelAddressID.Name = "labelAddressID";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtZipCode
            // 
            resources.ApplyResources(this.txtZipCode, "txtZipCode");
            this.txtZipCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtZipCode.Name = "txtZipCode";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtState
            // 
            resources.ApplyResources(this.txtState, "txtState");
            this.txtState.Name = "txtState";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtLine2
            // 
            resources.ApplyResources(this.txtLine2, "txtLine2");
            this.txtLine2.Name = "txtLine2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtLine1
            // 
            resources.ApplyResources(this.txtLine1, "txtLine1");
            this.txtLine1.Name = "txtLine1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddressEditAddress
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpBoxDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddressEditAddress";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddressEditAddress_FormClosing);
            this.Load += new System.EventHandler(this.frmAddressEditAddress_Load);
            this.grpBoxDetails.ResumeLayout(false);
            this.grpBoxDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLine2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLine1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label labelAddressID;
    }
}