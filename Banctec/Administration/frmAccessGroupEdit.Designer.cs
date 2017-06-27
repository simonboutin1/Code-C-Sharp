namespace Administration
{
    partial class frmAccessGroupEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccessGroupEdit));
            this.labelSortPattern = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveAllLeft = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvSelectedAccessCodes = new System.Windows.Forms.DataGridView();
            this.dgvAvailableAccessCodes = new System.Windows.Forms.DataGridView();
            this.btnMoveAllRight = new System.Windows.Forms.Button();
            this.btnMoveOneLeft = new System.Windows.Forms.Button();
            this.btnMoveOneRight = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedAccessCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableAccessCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSortPattern
            // 
            resources.ApplyResources(this.labelSortPattern, "labelSortPattern");
            this.labelSortPattern.Name = "labelSortPattern";
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
            // btnMoveAllLeft
            // 
            resources.ApplyResources(this.btnMoveAllLeft, "btnMoveAllLeft");
            this.btnMoveAllLeft.Name = "btnMoveAllLeft";
            this.btnMoveAllLeft.UseVisualStyleBackColor = true;
            this.btnMoveAllLeft.Click += new System.EventHandler(this.btnMoveAllLeft_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvSelectedAccessCodes);
            this.groupBox1.Controls.Add(this.dgvAvailableAccessCodes);
            this.groupBox1.Controls.Add(this.btnMoveAllLeft);
            this.groupBox1.Controls.Add(this.btnMoveAllRight);
            this.groupBox1.Controls.Add(this.btnMoveOneLeft);
            this.groupBox1.Controls.Add(this.btnMoveOneRight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // dgvSelectedAccessCodes
            // 
            this.dgvSelectedAccessCodes.AllowUserToAddRows = false;
            this.dgvSelectedAccessCodes.AllowUserToDeleteRows = false;
            this.dgvSelectedAccessCodes.AllowUserToResizeRows = false;
            this.dgvSelectedAccessCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedAccessCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvSelectedAccessCodes, "dgvSelectedAccessCodes");
            this.dgvSelectedAccessCodes.MultiSelect = false;
            this.dgvSelectedAccessCodes.Name = "dgvSelectedAccessCodes";
            this.dgvSelectedAccessCodes.ReadOnly = true;
            this.dgvSelectedAccessCodes.RowHeadersVisible = false;
            this.dgvSelectedAccessCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // dgvAvailableAccessCodes
            // 
            this.dgvAvailableAccessCodes.AllowUserToAddRows = false;
            this.dgvAvailableAccessCodes.AllowUserToDeleteRows = false;
            this.dgvAvailableAccessCodes.AllowUserToResizeRows = false;
            this.dgvAvailableAccessCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableAccessCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvAvailableAccessCodes, "dgvAvailableAccessCodes");
            this.dgvAvailableAccessCodes.MultiSelect = false;
            this.dgvAvailableAccessCodes.Name = "dgvAvailableAccessCodes";
            this.dgvAvailableAccessCodes.ReadOnly = true;
            this.dgvAvailableAccessCodes.RowHeadersVisible = false;
            this.dgvAvailableAccessCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnMoveAllRight
            // 
            resources.ApplyResources(this.btnMoveAllRight, "btnMoveAllRight");
            this.btnMoveAllRight.Name = "btnMoveAllRight";
            this.btnMoveAllRight.UseVisualStyleBackColor = true;
            this.btnMoveAllRight.Click += new System.EventHandler(this.btnMoveAllRight_Click);
            // 
            // btnMoveOneLeft
            // 
            resources.ApplyResources(this.btnMoveOneLeft, "btnMoveOneLeft");
            this.btnMoveOneLeft.Name = "btnMoveOneLeft";
            this.btnMoveOneLeft.UseVisualStyleBackColor = true;
            this.btnMoveOneLeft.Click += new System.EventHandler(this.btnMoveOneLeft_Click);
            // 
            // btnMoveOneRight
            // 
            resources.ApplyResources(this.btnMoveOneRight, "btnMoveOneRight");
            this.btnMoveOneRight.Name = "btnMoveOneRight";
            this.btnMoveOneRight.UseVisualStyleBackColor = true;
            this.btnMoveOneRight.Click += new System.EventHandler(this.btnMoveOneRight_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            this.txtID.Validating += new System.ComponentModel.CancelEventHandler(this.txtID_Validating);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // frmAccessGroupEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelSortPattern);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAccessGroupEdit";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAccessGroupEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmAccessGroupEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedAccessCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableAccessCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSortPattern;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoveAllLeft;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMoveAllRight;
        private System.Windows.Forms.Button btnMoveOneLeft;
        private System.Windows.Forms.Button btnMoveOneRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dgvSelectedAccessCodes;
        public System.Windows.Forms.DataGridView dgvAvailableAccessCodes;
    }
}