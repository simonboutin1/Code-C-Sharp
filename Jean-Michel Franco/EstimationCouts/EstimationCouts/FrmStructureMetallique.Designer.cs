namespace EstimationCouts
{
    partial class FrmStructureMetallique
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
            System.Windows.Forms.Label orientationTissuIDLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.tableEstimationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet = new EstimationCouts.BD_Estimation_CoutsDataSet();
            this.tableEstimationTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSetTableAdapters.TableEstimationTableAdapter();
            this.buttonFermer = new System.Windows.Forms.Button();
            this.labelTitre = new System.Windows.Forms.Label();
            this.tableStructMetalTypeMetalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bDEstimationCoutsDataSetTypeMetalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet_TypeMetal = new EstimationCouts.BD_Estimation_CoutsDataSet_TypeMetal();
            this.comboForme = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.tableStructMetalTypeMetalTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSet_TypeMetalTableAdapters.TableStructMetalTypeMetalTableAdapter();
            this.comboTypeMetal = new System.Windows.Forms.ComboBox();
            this.tableStructMetalTypeMetalBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            orientationTissuIDLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tableEstimationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableStructMetalTypeMetalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetTypeMetalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_TypeMetal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableStructMetalTypeMetalBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // orientationTissuIDLabel
            // 
            orientationTissuIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            orientationTissuIDLabel.Location = new System.Drawing.Point(16, 107);
            orientationTissuIDLabel.Name = "orientationTissuIDLabel";
            orientationTissuIDLabel.Size = new System.Drawing.Size(142, 21);
            orientationTissuIDLabel.TabIndex = 102;
            orientationTissuIDLabel.Text = "Type de métal:";
            orientationTissuIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(16, 137);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(142, 21);
            label1.TabIndex = 105;
            label1.Text = "Forme:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(16, 167);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(142, 21);
            label2.TabIndex = 107;
            label2.Text = "Dimension:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(16, 197);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(142, 21);
            label3.TabIndex = 109;
            label3.Text = "Type d\'alliage:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // tableEstimationTableAdapter
            // 
            this.tableEstimationTableAdapter.ClearBeforeFill = true;
            // 
            // buttonFermer
            // 
            this.buttonFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFermer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonFermer.Location = new System.Drawing.Point(170, 315);
            this.buttonFermer.Name = "buttonFermer";
            this.buttonFermer.Size = new System.Drawing.Size(110, 35);
            this.buttonFermer.TabIndex = 4;
            this.buttonFermer.Text = "Fermer";
            this.buttonFermer.UseVisualStyleBackColor = true;
            this.buttonFermer.Click += new System.EventHandler(this.buttonFermer_Click);
            // 
            // labelTitre
            // 
            this.labelTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelTitre.Location = new System.Drawing.Point(0, 9);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(434, 24);
            this.labelTitre.TabIndex = 100;
            this.labelTitre.Text = "Structure métallique";
            this.labelTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableStructMetalTypeMetalBindingSource
            // 
            this.tableStructMetalTypeMetalBindingSource.DataMember = "TableStructMetalTypeMetal";
            this.tableStructMetalTypeMetalBindingSource.DataSource = this.bDEstimationCoutsDataSetTypeMetalBindingSource;
            // 
            // bDEstimationCoutsDataSetTypeMetalBindingSource
            // 
            this.bDEstimationCoutsDataSetTypeMetalBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_TypeMetal;
            this.bDEstimationCoutsDataSetTypeMetalBindingSource.Position = 0;
            // 
            // bD_Estimation_CoutsDataSet_TypeMetal
            // 
            this.bD_Estimation_CoutsDataSet_TypeMetal.DataSetName = "BD_Estimation_CoutsDataSet_TypeMetal";
            this.bD_Estimation_CoutsDataSet_TypeMetal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboForme
            // 
            this.comboForme.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboForme.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "StructMetalForme", true));
            this.comboForme.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboForme.FormattingEnabled = true;
            this.comboForme.Location = new System.Drawing.Point(164, 136);
            this.comboForme.Name = "comboForme";
            this.comboForme.Size = new System.Drawing.Size(121, 24);
            this.comboForme.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.DisplayMember = "Description";
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(164, 166);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 24);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.ValueMember = "ID";
            // 
            // comboBox3
            // 
            this.comboBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox3.DisplayMember = "Description";
            this.comboBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(164, 196);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 24);
            this.comboBox3.TabIndex = 3;
            this.comboBox3.ValueMember = "ID";
            // 
            // tableStructMetalTypeMetalTableAdapter
            // 
            this.tableStructMetalTypeMetalTableAdapter.ClearBeforeFill = true;
            // 
            // comboTypeMetal
            // 
            this.comboTypeMetal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboTypeMetal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tableEstimationBindingSource, "StructMetalTypeMetal", true));
            this.comboTypeMetal.DataSource = this.tableStructMetalTypeMetalBindingSource1;
            this.comboTypeMetal.DisplayMember = "Description";
            this.comboTypeMetal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypeMetal.FormattingEnabled = true;
            this.comboTypeMetal.Location = new System.Drawing.Point(164, 106);
            this.comboTypeMetal.Name = "comboTypeMetal";
            this.comboTypeMetal.Size = new System.Drawing.Size(121, 24);
            this.comboTypeMetal.TabIndex = 110;
            this.comboTypeMetal.ValueMember = "Description";
            // 
            // tableStructMetalTypeMetalBindingSource1
            // 
            this.tableStructMetalTypeMetalBindingSource1.DataMember = "TableStructMetalTypeMetal";
            this.tableStructMetalTypeMetalBindingSource1.DataSource = this.bD_Estimation_CoutsDataSet_TypeMetal;
            // 
            // FrmStructureMetallique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 362);
            this.Controls.Add(this.comboTypeMetal);
            this.Controls.Add(label3);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(label1);
            this.Controls.Add(this.comboForme);
            this.Controls.Add(orientationTissuIDLabel);
            this.Controls.Add(this.labelTitre);
            this.Controls.Add(this.buttonFermer);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 400);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "FrmStructureMetallique";
            this.ShowIcon = false;
            this.Text = "Structure métallique";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStructureMetallique_FormClosing);
            this.Load += new System.EventHandler(this.FrmStructureMetallique_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableEstimationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableStructMetalTypeMetalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetTypeMetalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_TypeMetal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableStructMetalTypeMetalBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource tableEstimationBindingSource;
        private BD_Estimation_CoutsDataSet bD_Estimation_CoutsDataSet;
        private BD_Estimation_CoutsDataSetTableAdapters.TableEstimationTableAdapter tableEstimationTableAdapter;
        private System.Windows.Forms.Button buttonFermer;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.ComboBox comboForme;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetTypeMetalBindingSource;
        private BD_Estimation_CoutsDataSet_TypeMetal bD_Estimation_CoutsDataSet_TypeMetal;
        private System.Windows.Forms.BindingSource tableStructMetalTypeMetalBindingSource;
        private BD_Estimation_CoutsDataSet_TypeMetalTableAdapters.TableStructMetalTypeMetalTableAdapter tableStructMetalTypeMetalTableAdapter;
        private System.Windows.Forms.ComboBox comboTypeMetal;
        private System.Windows.Forms.BindingSource tableStructMetalTypeMetalBindingSource1;
    }
}