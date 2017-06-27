namespace Administration
{
    partial class frmSortPatternCond
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSortPatternCond));
            this.txtSequence = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboConditionField = new System.Windows.Forms.ComboBox();
            this.cboLogicOperator = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtValue1 = new System.Windows.Forms.TextBox();
            this.labelValue1 = new System.Windows.Forms.Label();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.labelValue2 = new System.Windows.Forms.Label();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.listListValues = new System.Windows.Forms.ListBox();
            this.labelListValues = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.labelValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboEndPoint = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtSequence
            // 
            resources.ApplyResources(this.txtSequence, "txtSequence");
            this.txtSequence.Name = "txtSequence";
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
            // cboConditionField
            // 
            this.cboConditionField.BackColor = System.Drawing.SystemColors.Window;
            this.cboConditionField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConditionField.FormattingEnabled = true;
            resources.ApplyResources(this.cboConditionField, "cboConditionField");
            this.cboConditionField.Name = "cboConditionField";
            this.cboConditionField.SelectedValueChanged += new System.EventHandler(this.cboConditionField_SelectedValueChanged);
            // 
            // cboLogicOperator
            // 
            this.cboLogicOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogicOperator.FormattingEnabled = true;
            resources.ApplyResources(this.cboLogicOperator, "cboLogicOperator");
            this.cboLogicOperator.Name = "cboLogicOperator";
            this.cboLogicOperator.SelectedValueChanged += new System.EventHandler(this.cboLogicOperator_SelectedValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtValue1
            // 
            resources.ApplyResources(this.txtValue1, "txtValue1");
            this.txtValue1.Name = "txtValue1";
            // 
            // labelValue1
            // 
            resources.ApplyResources(this.labelValue1, "labelValue1");
            this.labelValue1.Name = "labelValue1";
            // 
            // btnAction
            // 
            resources.ApplyResources(this.btnAction, "btnAction");
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
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // labelValue2
            // 
            resources.ApplyResources(this.labelValue2, "labelValue2");
            this.labelValue2.Name = "labelValue2";
            // 
            // txtValue2
            // 
            resources.ApplyResources(this.txtValue2, "txtValue2");
            this.txtValue2.Name = "txtValue2";
            // 
            // listListValues
            // 
            this.listListValues.BackColor = System.Drawing.SystemColors.Window;
            this.listListValues.FormattingEnabled = true;
            resources.ApplyResources(this.listListValues, "listListValues");
            this.listListValues.Name = "listListValues";
            this.listListValues.SelectedIndexChanged += new System.EventHandler(this.listListValues_SelectedIndexChanged);
            // 
            // labelListValues
            // 
            resources.ApplyResources(this.labelListValues, "labelListValues");
            this.labelListValues.Name = "labelListValues";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // labelValue
            // 
            resources.ApplyResources(this.labelValue, "labelValue");
            this.labelValue.Name = "labelValue";
            // 
            // txtValue
            // 
            resources.ApplyResources(this.txtValue, "txtValue");
            this.txtValue.Name = "txtValue";
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // cboEndPoint
            // 
            this.cboEndPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEndPoint.FormattingEnabled = true;
            resources.ApplyResources(this.cboEndPoint, "cboEndPoint");
            this.cboEndPoint.Name = "cboEndPoint";
            // 
            // frmSortPatternCond
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboEndPoint);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelListValues);
            this.Controls.Add(this.listListValues);
            this.Controls.Add(this.labelValue2);
            this.Controls.Add(this.txtValue2);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelValue1);
            this.Controls.Add(this.txtValue1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboLogicOperator);
            this.Controls.Add(this.cboConditionField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSequence);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSortPatternCond";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSortPatternCond_FormClosing);
            this.Load += new System.EventHandler(this.frmSortPatternCond_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSequence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboConditionField;
        private System.Windows.Forms.ComboBox cboLogicOperator;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtValue1;
        private System.Windows.Forms.Label labelValue1;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label labelValue2;
        private System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.ListBox listListValues;
        private System.Windows.Forms.Label labelListValues;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cboEndPoint;
    }
}