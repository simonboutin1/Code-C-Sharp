namespace EstimationCouts
{
    partial class FrmDimensionsForme
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
            this.components = new System.ComponentModel.Container();
            this.labelTitre = new System.Windows.Forms.Label();
            this.textDimension1 = new System.Windows.Forms.TextBox();
            this.tableEstimationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet = new EstimationCouts.BD_Estimation_CoutsDataSet();
            this.labelDimension1 = new System.Windows.Forms.Label();
            this.labelPouces = new System.Windows.Forms.Label();
            this.labelDimension2 = new System.Windows.Forms.Label();
            this.textDimension2 = new System.Windows.Forms.TextBox();
            this.labelDimension3 = new System.Windows.Forms.Label();
            this.textDimension3 = new System.Windows.Forms.TextBox();
            this.labelDimension4 = new System.Windows.Forms.Label();
            this.textDimension4 = new System.Windows.Forms.TextBox();
            this.labelDimension5 = new System.Windows.Forms.Label();
            this.textDimension5 = new System.Windows.Forms.TextBox();
            this.buttonFermer = new System.Windows.Forms.Button();
            this.tableEstimationTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSetTableAdapters.TableEstimationTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tableEstimationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitre
            // 
            this.labelTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelTitre.Location = new System.Drawing.Point(-1, 9);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(434, 24);
            this.labelTitre.TabIndex = 100;
            this.labelTitre.Text = "Forme";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textDimension1
            // 
            this.textDimension1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "Dimension1", true));
            this.textDimension1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDimension1.Location = new System.Drawing.Point(186, 106);
            this.textDimension1.Name = "textDimension1";
            this.textDimension1.Size = new System.Drawing.Size(68, 23);
            this.textDimension1.TabIndex = 0;
            this.textDimension1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDimension1.Visible = false;
            this.textDimension1.Validating += new System.ComponentModel.CancelEventHandler(this.textDimension1_Validating);
            // 
            // tableEstimationBindingSource
            // 
            this.tableEstimationBindingSource.DataMember = "TableEstimation";
            this.tableEstimationBindingSource.DataSource = this.bD_Estimation_CoutsDataSet;
            // 
            // bD_Estimation_CoutsDataSet
            // 
            this.bD_Estimation_CoutsDataSet.DataSetName = "BD_Estimation_CoutsDataSet";
            this.bD_Estimation_CoutsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelDimension1
            // 
            this.labelDimension1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimension1.Location = new System.Drawing.Point(16, 106);
            this.labelDimension1.Name = "labelDimension1";
            this.labelDimension1.Size = new System.Drawing.Size(167, 20);
            this.labelDimension1.TabIndex = 100;
            this.labelDimension1.Text = "Dimension 1:";
            this.labelDimension1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelDimension1.Visible = false;
            // 
            // labelPouces
            // 
            this.labelPouces.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPouces.Location = new System.Drawing.Point(186, 82);
            this.labelPouces.Name = "labelPouces";
            this.labelPouces.Size = new System.Drawing.Size(68, 21);
            this.labelPouces.TabIndex = 100;
            this.labelPouces.Text = "(pouces)";
            this.labelPouces.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDimension2
            // 
            this.labelDimension2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimension2.Location = new System.Drawing.Point(16, 135);
            this.labelDimension2.Name = "labelDimension2";
            this.labelDimension2.Size = new System.Drawing.Size(167, 20);
            this.labelDimension2.TabIndex = 100;
            this.labelDimension2.Text = "Dimension 2:";
            this.labelDimension2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelDimension2.Visible = false;
            // 
            // textDimension2
            // 
            this.textDimension2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "Dimension2", true));
            this.textDimension2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDimension2.Location = new System.Drawing.Point(186, 135);
            this.textDimension2.Name = "textDimension2";
            this.textDimension2.Size = new System.Drawing.Size(68, 23);
            this.textDimension2.TabIndex = 1;
            this.textDimension2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDimension2.Visible = false;
            this.textDimension2.Validating += new System.ComponentModel.CancelEventHandler(this.textDimension2_Validating);
            // 
            // labelDimension3
            // 
            this.labelDimension3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimension3.Location = new System.Drawing.Point(16, 164);
            this.labelDimension3.Name = "labelDimension3";
            this.labelDimension3.Size = new System.Drawing.Size(167, 20);
            this.labelDimension3.TabIndex = 100;
            this.labelDimension3.Text = "Dimension 3:";
            this.labelDimension3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelDimension3.Visible = false;
            // 
            // textDimension3
            // 
            this.textDimension3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "Dimension3", true));
            this.textDimension3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDimension3.Location = new System.Drawing.Point(186, 164);
            this.textDimension3.Name = "textDimension3";
            this.textDimension3.Size = new System.Drawing.Size(68, 23);
            this.textDimension3.TabIndex = 2;
            this.textDimension3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDimension3.Visible = false;
            this.textDimension3.Validating += new System.ComponentModel.CancelEventHandler(this.textDimension3_Validating);
            // 
            // labelDimension4
            // 
            this.labelDimension4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimension4.Location = new System.Drawing.Point(16, 193);
            this.labelDimension4.Name = "labelDimension4";
            this.labelDimension4.Size = new System.Drawing.Size(167, 20);
            this.labelDimension4.TabIndex = 100;
            this.labelDimension4.Text = "Dimension 4:";
            this.labelDimension4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelDimension4.Visible = false;
            // 
            // textDimension4
            // 
            this.textDimension4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "Dimension4", true));
            this.textDimension4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDimension4.Location = new System.Drawing.Point(186, 193);
            this.textDimension4.Name = "textDimension4";
            this.textDimension4.Size = new System.Drawing.Size(68, 23);
            this.textDimension4.TabIndex = 3;
            this.textDimension4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDimension4.Visible = false;
            this.textDimension4.Validating += new System.ComponentModel.CancelEventHandler(this.textDimension4_Validating);
            // 
            // labelDimension5
            // 
            this.labelDimension5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimension5.Location = new System.Drawing.Point(16, 222);
            this.labelDimension5.Name = "labelDimension5";
            this.labelDimension5.Size = new System.Drawing.Size(167, 20);
            this.labelDimension5.TabIndex = 100;
            this.labelDimension5.Text = "Dimension 5:";
            this.labelDimension5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelDimension5.Visible = false;
            // 
            // textDimension5
            // 
            this.textDimension5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "Dimension5", true));
            this.textDimension5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDimension5.Location = new System.Drawing.Point(186, 222);
            this.textDimension5.Name = "textDimension5";
            this.textDimension5.Size = new System.Drawing.Size(68, 23);
            this.textDimension5.TabIndex = 4;
            this.textDimension5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDimension5.Visible = false;
            this.textDimension5.Validating += new System.ComponentModel.CancelEventHandler(this.textDimension5_Validating);
            // 
            // buttonFermer
            // 
            this.buttonFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFermer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonFermer.Location = new System.Drawing.Point(170, 315);
            this.buttonFermer.Name = "buttonFermer";
            this.buttonFermer.Size = new System.Drawing.Size(110, 35);
            this.buttonFermer.TabIndex = 5;
            this.buttonFermer.Text = "Fermer";
            this.buttonFermer.UseVisualStyleBackColor = true;
            this.buttonFermer.Click += new System.EventHandler(this.buttonFermer_Click);
            // 
            // tableEstimationTableAdapter
            // 
            this.tableEstimationTableAdapter.ClearBeforeFill = true;
            // 
            // FrmDimensionsForme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 362);
            this.Controls.Add(this.buttonFermer);
            this.Controls.Add(this.labelDimension5);
            this.Controls.Add(this.textDimension5);
            this.Controls.Add(this.labelDimension4);
            this.Controls.Add(this.textDimension4);
            this.Controls.Add(this.labelDimension3);
            this.Controls.Add(this.textDimension3);
            this.Controls.Add(this.labelDimension2);
            this.Controls.Add(this.textDimension2);
            this.Controls.Add(this.labelPouces);
            this.Controls.Add(this.labelDimension1);
            this.Controls.Add(this.textDimension1);
            this.Controls.Add(this.labelTitre);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 400);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "FrmDimensionsForme";
            this.ShowIcon = false;
            this.Text = "Dimensions de la forme";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDimensionsForme_FormClosing);
            this.Load += new System.EventHandler(this.FrmDimensionsForme_Load);
            this.Shown += new System.EventHandler(this.FrmDimensionsForme_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.tableEstimationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.TextBox textDimension1;
        private System.Windows.Forms.Label labelDimension1;
        private System.Windows.Forms.Label labelPouces;
        private System.Windows.Forms.Label labelDimension2;
        private System.Windows.Forms.TextBox textDimension2;
        private System.Windows.Forms.Label labelDimension3;
        private System.Windows.Forms.TextBox textDimension3;
        private System.Windows.Forms.Label labelDimension4;
        private System.Windows.Forms.TextBox textDimension4;
        private System.Windows.Forms.Label labelDimension5;
        private System.Windows.Forms.TextBox textDimension5;
        private System.Windows.Forms.Button buttonFermer;
        private BD_Estimation_CoutsDataSet bD_Estimation_CoutsDataSet;
        private System.Windows.Forms.BindingSource tableEstimationBindingSource;
        private BD_Estimation_CoutsDataSetTableAdapters.TableEstimationTableAdapter tableEstimationTableAdapter;
    }
}