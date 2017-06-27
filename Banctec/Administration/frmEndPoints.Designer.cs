namespace Administration
{
    partial class frmEndPoints
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEndPoints));
            this.labelEndPoints = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvEndPoints = new BancTec.PCR2P.Core.NetControls.customDGV();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // labelEndPoints
            // 
            resources.ApplyResources(this.labelEndPoints, "labelEndPoints");
            this.labelEndPoints.Name = "labelEndPoints";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvEndPoints
            // 
            this.dgvEndPoints.AllowUserToAddRows = false;
            this.dgvEndPoints.AllowUserToDeleteRows = false;
            this.dgvEndPoints.AllowUserToResizeColumns = false;
            this.dgvEndPoints.AllowUserToResizeRows = false;
            this.dgvEndPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvEndPoints, "dgvEndPoints");
            this.dgvEndPoints.MultiSelect = false;
            this.dgvEndPoints.Name = "dgvEndPoints";
            this.dgvEndPoints.ReadOnly = true;
            this.dgvEndPoints.RowHeadersVisible = false;
            this.dgvEndPoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEndPoints.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEndPoints_CellDoubleClick);
            this.dgvEndPoints.SelectionChanged += new System.EventHandler(this.dgvEndPoints_SelectionChanged);
            // 
            // frmEndPoints
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelEndPoints);
            this.Controls.Add(this.dgvEndPoints);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEndPoints";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmEndPoints_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEndPoints;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClose;
        private BancTec.PCR2P.Core.NetControls.customDGV dgvEndPoints;
    }
}