namespace Administration
{
    partial class frmSortPatternEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSortPatternEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBoxSummary = new System.Windows.Forms.GroupBox();
            this.txtRejects = new System.Windows.Forms.TextBox();
            this.txtVirtual = new System.Windows.Forms.TextBox();
            this.txtPhysical = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioPhysical = new System.Windows.Forms.RadioButton();
            this.radioVirtual = new System.Windows.Forms.RadioButton();
            this.panelPockets = new System.Windows.Forms.Panel();
            this.labelPockets = new System.Windows.Forms.Label();
            this.panelConditions = new System.Windows.Forms.Panel();
            this.radioOr = new System.Windows.Forms.RadioButton();
            this.radioAnd = new System.Windows.Forms.RadioButton();
            this.labelConditions = new System.Windows.Forms.Label();
            this.btnRemovePocket = new System.Windows.Forms.Button();
            this.btnEditPocket = new System.Windows.Forms.Button();
            this.btnAddPocket = new System.Windows.Forms.Button();
            this.btnMoveUpPocket = new System.Windows.Forms.Button();
            this.btnMoveDownPocket = new System.Windows.Forms.Button();
            this.btnRemoveCondition = new System.Windows.Forms.Button();
            this.btnEditCondition = new System.Windows.Forms.Button();
            this.btnAddCondition = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dgvPockets = new System.Windows.Forms.DataGridView();
            this.dgvConditions = new System.Windows.Forms.DataGridView();
            this.grpBoxSummary.SuspendLayout();
            this.panelPockets.SuspendLayout();
            this.panelConditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPockets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditions)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // grpBoxSummary
            // 
            this.grpBoxSummary.Controls.Add(this.txtRejects);
            this.grpBoxSummary.Controls.Add(this.txtVirtual);
            this.grpBoxSummary.Controls.Add(this.txtPhysical);
            this.grpBoxSummary.Controls.Add(this.label6);
            this.grpBoxSummary.Controls.Add(this.label5);
            this.grpBoxSummary.Controls.Add(this.label4);
            this.grpBoxSummary.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.grpBoxSummary, "grpBoxSummary");
            this.grpBoxSummary.Name = "grpBoxSummary";
            this.grpBoxSummary.TabStop = false;
            // 
            // txtRejects
            // 
            resources.ApplyResources(this.txtRejects, "txtRejects");
            this.txtRejects.Name = "txtRejects";
            // 
            // txtVirtual
            // 
            resources.ApplyResources(this.txtVirtual, "txtVirtual");
            this.txtVirtual.Name = "txtVirtual";
            // 
            // txtPhysical
            // 
            resources.ApplyResources(this.txtPhysical, "txtPhysical");
            this.txtPhysical.Name = "txtPhysical";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnAction
            // 
            resources.ApplyResources(this.btnAction, "btnAction");
            this.btnAction.ForeColor = System.Drawing.Color.Black;
            this.btnAction.Name = "btnAction";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // radioPhysical
            // 
            resources.ApplyResources(this.radioPhysical, "radioPhysical");
            this.radioPhysical.Name = "radioPhysical";
            this.radioPhysical.TabStop = true;
            this.radioPhysical.UseVisualStyleBackColor = true;
            // 
            // radioVirtual
            // 
            resources.ApplyResources(this.radioVirtual, "radioVirtual");
            this.radioVirtual.Name = "radioVirtual";
            this.radioVirtual.TabStop = true;
            this.radioVirtual.UseVisualStyleBackColor = true;
            // 
            // panelPockets
            // 
            this.panelPockets.Controls.Add(this.radioVirtual);
            this.panelPockets.Controls.Add(this.radioPhysical);
            resources.ApplyResources(this.panelPockets, "panelPockets");
            this.panelPockets.Name = "panelPockets";
            // 
            // labelPockets
            // 
            resources.ApplyResources(this.labelPockets, "labelPockets");
            this.labelPockets.Name = "labelPockets";
            // 
            // panelConditions
            // 
            this.panelConditions.Controls.Add(this.radioOr);
            this.panelConditions.Controls.Add(this.radioAnd);
            resources.ApplyResources(this.panelConditions, "panelConditions");
            this.panelConditions.Name = "panelConditions";
            // 
            // radioOr
            // 
            resources.ApplyResources(this.radioOr, "radioOr");
            this.radioOr.Name = "radioOr";
            this.radioOr.TabStop = true;
            this.radioOr.UseVisualStyleBackColor = true;
            // 
            // radioAnd
            // 
            resources.ApplyResources(this.radioAnd, "radioAnd");
            this.radioAnd.Name = "radioAnd";
            this.radioAnd.TabStop = true;
            this.radioAnd.UseVisualStyleBackColor = true;
            // 
            // labelConditions
            // 
            resources.ApplyResources(this.labelConditions, "labelConditions");
            this.labelConditions.Name = "labelConditions";
            // 
            // btnRemovePocket
            // 
            resources.ApplyResources(this.btnRemovePocket, "btnRemovePocket");
            this.btnRemovePocket.Name = "btnRemovePocket";
            this.btnRemovePocket.UseVisualStyleBackColor = true;
            this.btnRemovePocket.Click += new System.EventHandler(this.btnRemovePocket_Click);
            // 
            // btnEditPocket
            // 
            resources.ApplyResources(this.btnEditPocket, "btnEditPocket");
            this.btnEditPocket.Name = "btnEditPocket";
            this.btnEditPocket.UseVisualStyleBackColor = true;
            this.btnEditPocket.Click += new System.EventHandler(this.btnEditPocket_Click);
            // 
            // btnAddPocket
            // 
            resources.ApplyResources(this.btnAddPocket, "btnAddPocket");
            this.btnAddPocket.Name = "btnAddPocket";
            this.btnAddPocket.UseVisualStyleBackColor = true;
            this.btnAddPocket.Click += new System.EventHandler(this.btnAddPocket_Click);
            // 
            // btnMoveUpPocket
            // 
            resources.ApplyResources(this.btnMoveUpPocket, "btnMoveUpPocket");
            this.btnMoveUpPocket.Name = "btnMoveUpPocket";
            this.btnMoveUpPocket.UseVisualStyleBackColor = true;
            this.btnMoveUpPocket.Click += new System.EventHandler(this.btnMoveUpPocket_Click);
            // 
            // btnMoveDownPocket
            // 
            resources.ApplyResources(this.btnMoveDownPocket, "btnMoveDownPocket");
            this.btnMoveDownPocket.Name = "btnMoveDownPocket";
            this.btnMoveDownPocket.UseVisualStyleBackColor = true;
            this.btnMoveDownPocket.Click += new System.EventHandler(this.btnMoveDownPocket_Click);
            // 
            // btnRemoveCondition
            // 
            resources.ApplyResources(this.btnRemoveCondition, "btnRemoveCondition");
            this.btnRemoveCondition.Name = "btnRemoveCondition";
            this.btnRemoveCondition.UseVisualStyleBackColor = true;
            this.btnRemoveCondition.Click += new System.EventHandler(this.btnRemoveCondition_Click);
            // 
            // btnEditCondition
            // 
            resources.ApplyResources(this.btnEditCondition, "btnEditCondition");
            this.btnEditCondition.Name = "btnEditCondition";
            this.btnEditCondition.UseVisualStyleBackColor = true;
            this.btnEditCondition.Click += new System.EventHandler(this.btnEditCondition_Click);
            // 
            // btnAddCondition
            // 
            resources.ApplyResources(this.btnAddCondition, "btnAddCondition");
            this.btnAddCondition.Name = "btnAddCondition";
            this.btnAddCondition.UseVisualStyleBackColor = true;
            this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // dgvPockets
            // 
            this.dgvPockets.AllowUserToAddRows = false;
            this.dgvPockets.AllowUserToDeleteRows = false;
            this.dgvPockets.AllowUserToResizeRows = false;
            this.dgvPockets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPockets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvPockets, "dgvPockets");
            this.dgvPockets.MultiSelect = false;
            this.dgvPockets.Name = "dgvPockets";
            this.dgvPockets.ReadOnly = true;
            this.dgvPockets.RowHeadersVisible = false;
            this.dgvPockets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPockets.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPockets_CellDoubleClick);
            this.dgvPockets.CurrentCellChanged += new System.EventHandler(this.dgvPockets_CurrentCellChanged);
            // 
            // dgvConditions
            // 
            this.dgvConditions.AllowUserToAddRows = false;
            this.dgvConditions.AllowUserToDeleteRows = false;
            this.dgvConditions.AllowUserToResizeRows = false;
            this.dgvConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvConditions, "dgvConditions");
            this.dgvConditions.MultiSelect = false;
            this.dgvConditions.Name = "dgvConditions";
            this.dgvConditions.ReadOnly = true;
            this.dgvConditions.RowHeadersVisible = false;
            this.dgvConditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConditions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConditions_CellDoubleClick);
            // 
            // frmSortPatternEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvConditions);
            this.Controls.Add(this.dgvPockets);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnRemoveCondition);
            this.Controls.Add(this.btnEditCondition);
            this.Controls.Add(this.btnAddCondition);
            this.Controls.Add(this.btnMoveDownPocket);
            this.Controls.Add(this.btnMoveUpPocket);
            this.Controls.Add(this.btnRemovePocket);
            this.Controls.Add(this.btnEditPocket);
            this.Controls.Add(this.btnAddPocket);
            this.Controls.Add(this.labelConditions);
            this.Controls.Add(this.panelConditions);
            this.Controls.Add(this.labelPockets);
            this.Controls.Add(this.panelPockets);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.grpBoxSummary);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSortPatternEdit";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSortPatternEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmSortPatternEdit_Load);
            this.grpBoxSummary.ResumeLayout(false);
            this.grpBoxSummary.PerformLayout();
            this.panelPockets.ResumeLayout(false);
            this.panelPockets.PerformLayout();
            this.panelConditions.ResumeLayout(false);
            this.panelConditions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPockets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBoxSummary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton radioVirtual;
        private System.Windows.Forms.RadioButton radioPhysical;
        private System.Windows.Forms.Panel panelPockets;
        private System.Windows.Forms.Label labelPockets;
        private System.Windows.Forms.Panel panelConditions;
        private System.Windows.Forms.RadioButton radioOr;
        private System.Windows.Forms.RadioButton radioAnd;
        private System.Windows.Forms.Label labelConditions;
        private System.Windows.Forms.Button btnRemovePocket;
        private System.Windows.Forms.Button btnEditPocket;
        private System.Windows.Forms.Button btnAddPocket;
        private System.Windows.Forms.Button btnMoveUpPocket;
        private System.Windows.Forms.Button btnMoveDownPocket;
        private System.Windows.Forms.Button btnRemoveCondition;
        private System.Windows.Forms.Button btnEditCondition;
        private System.Windows.Forms.Button btnAddCondition;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtRejects;
        private System.Windows.Forms.TextBox txtVirtual;
        private System.Windows.Forms.TextBox txtPhysical;
        public System.Windows.Forms.DataGridView dgvPockets;
        public System.Windows.Forms.DataGridView dgvConditions;
    }
}