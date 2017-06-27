//à enlever (les 2 lignes)
//using System;
//using System.Windows.Forms;

namespace Administration
{
    partial class frmExchangeRateDef
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExchangeRateDef));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewRate = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpBoxDisplayOptions = new System.Windows.Forms.GroupBox();
            this.radioHistoricalRate = new System.Windows.Forms.RadioButton();
            this.radioCurrentRate = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grpBoxUser = new System.Windows.Forms.GroupBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.dgvRate = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpBoxDisplayOptions.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grpBoxUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRate)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvRate, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnNewRate);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnNewRate
            // 
            resources.ApplyResources(this.btnNewRate, "btnNewRate");
            this.btnNewRate.Name = "btnNewRate";
            this.btnNewRate.UseVisualStyleBackColor = true;
            this.btnNewRate.Click += new System.EventHandler(this.btnNewRate_Click);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grpBoxDisplayOptions);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // grpBoxDisplayOptions
            // 
            this.grpBoxDisplayOptions.Controls.Add(this.radioHistoricalRate);
            this.grpBoxDisplayOptions.Controls.Add(this.radioCurrentRate);
            this.grpBoxDisplayOptions.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.grpBoxDisplayOptions, "grpBoxDisplayOptions");
            this.grpBoxDisplayOptions.Name = "grpBoxDisplayOptions";
            this.grpBoxDisplayOptions.TabStop = false;
            // 
            // radioHistoricalRate
            // 
            resources.ApplyResources(this.radioHistoricalRate, "radioHistoricalRate");
            this.radioHistoricalRate.Name = "radioHistoricalRate";
            this.radioHistoricalRate.TabStop = true;
            this.radioHistoricalRate.UseVisualStyleBackColor = true;
            // 
            // radioCurrentRate
            // 
            resources.ApplyResources(this.radioCurrentRate, "radioCurrentRate");
            this.radioCurrentRate.Checked = true;
            this.radioCurrentRate.Name = "radioCurrentRate";
            this.radioCurrentRate.TabStop = true;
            this.radioCurrentRate.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grpBoxUser);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // grpBoxUser
            // 
            this.grpBoxUser.Controls.Add(this.labelUser);
            this.grpBoxUser.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.grpBoxUser, "grpBoxUser");
            this.grpBoxUser.Name = "grpBoxUser";
            this.grpBoxUser.TabStop = false;
            // 
            // labelUser
            // 
            resources.ApplyResources(this.labelUser, "labelUser");
            this.labelUser.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelUser.Name = "labelUser";
            // 
            // dgvRate
            // 
            this.dgvRate.AllowUserToAddRows = false;
            this.dgvRate.AllowUserToDeleteRows = false;
            this.dgvRate.AllowUserToResizeRows = false;
            this.dgvRate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvRate, "dgvRate");
            this.dgvRate.MultiSelect = false;
            this.dgvRate.Name = "dgvRate";
            this.dgvRate.ReadOnly = true;
            this.dgvRate.RowHeadersVisible = false;
            this.dgvRate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRate.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRate_CellDoubleClick);
            this.dgvRate.CurrentCellChanged += new System.EventHandler(this.dgvRate_CurrentCellChanged);
            // 
            // frmExchangeRateDef
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmExchangeRateDef";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmViewExchangeRate_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpBoxDisplayOptions.ResumeLayout(false);
            this.grpBoxDisplayOptions.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.grpBoxUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNewRate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox grpBoxDisplayOptions;
        private System.Windows.Forms.RadioButton radioHistoricalRate;
        private System.Windows.Forms.RadioButton radioCurrentRate;
        private System.Windows.Forms.GroupBox grpBoxUser;
        private System.Windows.Forms.Label labelUser;
        public System.Windows.Forms.DataGridView dgvRate;

    }
}