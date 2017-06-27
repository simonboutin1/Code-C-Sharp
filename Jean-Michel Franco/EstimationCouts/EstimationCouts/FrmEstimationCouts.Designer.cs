namespace EstimationCouts
{
    partial class FrmEstimationCouts
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label iDLabel;
            System.Windows.Forms.Label formeLabel;
            System.Windows.Forms.Label typeTissuLabel;
            System.Windows.Forms.Label orientationTissuIDLabel;
            System.Windows.Forms.Label typeAssemblageIDLabel;
            System.Windows.Forms.Label largeurStripLabel;
            System.Windows.Forms.Label siJointRetourneLabel;
            System.Windows.Forms.Label commentCollerJointIDLabel;
            System.Windows.Forms.Label couleurMetalLabel;
            System.Windows.Forms.Label structMetalTypeMetalLabel;
            System.Windows.Forms.Label structMetalFormeLabel;
            System.Windows.Forms.Label structMetalDimensionLabel;
            System.Windows.Forms.Label structMetalTypeAlliageIDLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstimationCouts));
            this.label1 = new System.Windows.Forms.Label();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet = new EstimationCouts.BD_Estimation_CoutsDataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.textID = new System.Windows.Forms.TextBox();
            this.textTypeTissu = new System.Windows.Forms.TextBox();
            this.checkSiJointRetourne = new System.Windows.Forms.CheckBox();
            this.structMetalTypeMetalTextBox = new System.Windows.Forms.TextBox();
            this.structMetalFormeTextBox = new System.Windows.Forms.TextBox();
            this.structMetalDimensionTextBox = new System.Windows.Forms.TextBox();
            this.structMetalTypeAlliageIDTextBox = new System.Windows.Forms.TextBox();
            this.comboForme = new System.Windows.Forms.ComboBox();
            this.tableFormeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet_Forme = new EstimationCouts.BD_Estimation_CoutsDataSet_Forme();
            this.tableEstimationTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSetTableAdapters.TableEstimationTableAdapter();
            this.tableAdapterManager = new EstimationCouts.BD_Estimation_CoutsDataSetTableAdapters.TableAdapterManager();
            this.tableFormeTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSet_FormeTableAdapters.TableFormeTableAdapter();
            this.buttonDimensions = new System.Windows.Forms.Button();
            this.labelAvertissement = new System.Windows.Forms.Label();
            this.buttonQuitter = new System.Windows.Forms.Button();
            this.comboOrientationTissu = new System.Windows.Forms.ComboBox();
            this.tableOrientationTissuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet_OrientationTissu = new EstimationCouts.BD_Estimation_CoutsDataSet_OrientationTissu();
            this.tableOrientationTissuBindingSource5 = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSet_OrientationTissuTableAdapters.TableOrientationTissuTableAdapter();
            this.tableEstimationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bDEstimationCoutsDataSetOrientationTissuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bDEstimationCoutsDataSetOrientationTissuBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bDEstimationCoutsDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuBindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuBindingSource6 = new System.Windows.Forms.BindingSource(this.components);
            this.tableOrientationTissuBindingSource7 = new System.Windows.Forms.BindingSource(this.components);
            this.comboTypeAssemblage = new System.Windows.Forms.ComboBox();
            this.tableTypeAssemblageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet_TypeAssemblage = new EstimationCouts.BD_Estimation_CoutsDataSet_TypeAssemblage();
            this.bDEstimationCoutsDataSetTypeAssemblageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableTypeAssemblageTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSet_TypeAssemblageTableAdapters.TableTypeAssemblageTableAdapter();
            this.comboLargeurStrip = new System.Windows.Forms.ComboBox();
            this.comboCommentCollerJoint = new System.Windows.Forms.ComboBox();
            this.bD_Estimation_CoutsDataSet_CommentCollerJoint = new EstimationCouts.BD_Estimation_CoutsDataSet_CommentCollerJoint();
            this.tableCommentCollerJointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableCommentCollerJointTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSet_CommentCollerJointTableAdapters.TableCommentCollerJointTableAdapter();
            this.comboCouleurMetal = new System.Windows.Forms.ComboBox();
            this.bD_Estimation_CoutsDataSet_CouleurMetal = new EstimationCouts.BD_Estimation_CoutsDataSet_CouleurMetal();
            this.bDEstimationCoutsDataSetCouleurMetalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bDEstimationCoutsDataSetCouleurMetalBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bD_Estimation_CoutsDataSet_CouleurMetal1 = new EstimationCouts.BD_Estimation_CoutsDataSet_CouleurMetal();
            this.bDEstimationCoutsDataSetCouleurMetal1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableCouleurMetalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableCouleurMetalTableAdapter = new EstimationCouts.BD_Estimation_CoutsDataSet_CouleurMetalTableAdapters.TableCouleurMetalTableAdapter();
            this.buttonStructureMetallique = new System.Windows.Forms.Button();
            iDLabel = new System.Windows.Forms.Label();
            formeLabel = new System.Windows.Forms.Label();
            typeTissuLabel = new System.Windows.Forms.Label();
            orientationTissuIDLabel = new System.Windows.Forms.Label();
            typeAssemblageIDLabel = new System.Windows.Forms.Label();
            largeurStripLabel = new System.Windows.Forms.Label();
            siJointRetourneLabel = new System.Windows.Forms.Label();
            commentCollerJointIDLabel = new System.Windows.Forms.Label();
            couleurMetalLabel = new System.Windows.Forms.Label();
            structMetalTypeMetalLabel = new System.Windows.Forms.Label();
            structMetalFormeLabel = new System.Windows.Forms.Label();
            structMetalDimensionLabel = new System.Windows.Forms.Label();
            structMetalTypeAlliageIDLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableFormeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_Forme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_OrientationTissu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableEstimationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetOrientationTissuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetOrientationTissuBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableTypeAssemblageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_TypeAssemblage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetTypeAssemblageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_CommentCollerJoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableCommentCollerJointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_CouleurMetal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetCouleurMetalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetCouleurMetalBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_CouleurMetal1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetCouleurMetal1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableCouleurMetalBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // iDLabel
            // 
            iDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            iDLabel.Location = new System.Drawing.Point(280, 268);
            iDLabel.Name = "iDLabel";
            iDLabel.Size = new System.Drawing.Size(142, 17);
            iDLabel.TabIndex = 2;
            iDLabel.Text = "ID:";
            iDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // formeLabel
            // 
            formeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            formeLabel.Location = new System.Drawing.Point(280, 294);
            formeLabel.Name = "formeLabel";
            formeLabel.Size = new System.Drawing.Size(142, 21);
            formeLabel.TabIndex = 4;
            formeLabel.Text = "Forme:";
            formeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeTissuLabel
            // 
            typeTissuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            typeTissuLabel.Location = new System.Drawing.Point(280, 355);
            typeTissuLabel.Name = "typeTissuLabel";
            typeTissuLabel.Size = new System.Drawing.Size(142, 20);
            typeTissuLabel.TabIndex = 16;
            typeTissuLabel.Text = "Type du tissu:";
            typeTissuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // orientationTissuIDLabel
            // 
            orientationTissuIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            orientationTissuIDLabel.Location = new System.Drawing.Point(280, 384);
            orientationTissuIDLabel.Name = "orientationTissuIDLabel";
            orientationTissuIDLabel.Size = new System.Drawing.Size(142, 21);
            orientationTissuIDLabel.TabIndex = 18;
            orientationTissuIDLabel.Text = "Orientation du tissu:";
            orientationTissuIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeAssemblageIDLabel
            // 
            typeAssemblageIDLabel.AutoSize = true;
            typeAssemblageIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            typeAssemblageIDLabel.Location = new System.Drawing.Point(287, 417);
            typeAssemblageIDLabel.Name = "typeAssemblageIDLabel";
            typeAssemblageIDLabel.Size = new System.Drawing.Size(135, 17);
            typeAssemblageIDLabel.TabIndex = 20;
            typeAssemblageIDLabel.Text = "Type d\'assemblage:";
            // 
            // largeurStripLabel
            // 
            largeurStripLabel.AutoSize = true;
            largeurStripLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            largeurStripLabel.Location = new System.Drawing.Point(309, 447);
            largeurStripLabel.Name = "largeurStripLabel";
            largeurStripLabel.Size = new System.Drawing.Size(113, 17);
            largeurStripLabel.TabIndex = 22;
            largeurStripLabel.Text = "Largeur du strip:";
            // 
            // siJointRetourneLabel
            // 
            siJointRetourneLabel.AutoSize = true;
            siJointRetourneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            siJointRetourneLabel.Location = new System.Drawing.Point(321, 477);
            siJointRetourneLabel.Name = "siJointRetourneLabel";
            siJointRetourneLabel.Size = new System.Drawing.Size(100, 17);
            siJointRetourneLabel.TabIndex = 24;
            siJointRetourneLabel.Text = "Joint retourné:";
            // 
            // commentCollerJointIDLabel
            // 
            commentCollerJointIDLabel.AutoSize = true;
            commentCollerJointIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            commentCollerJointIDLabel.Location = new System.Drawing.Point(268, 507);
            commentCollerJointIDLabel.Name = "commentCollerJointIDLabel";
            commentCollerJointIDLabel.Size = new System.Drawing.Size(154, 17);
            commentCollerJointIDLabel.TabIndex = 26;
            commentCollerJointIDLabel.Text = "Comment coller le joint:";
            // 
            // couleurMetalLabel
            // 
            couleurMetalLabel.AutoSize = true;
            couleurMetalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            couleurMetalLabel.Location = new System.Drawing.Point(303, 537);
            couleurMetalLabel.Name = "couleurMetalLabel";
            couleurMetalLabel.Size = new System.Drawing.Size(119, 17);
            couleurMetalLabel.TabIndex = 28;
            couleurMetalLabel.Text = "Couleur du métal:";
            // 
            // structMetalTypeMetalLabel
            // 
            structMetalTypeMetalLabel.AutoSize = true;
            structMetalTypeMetalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            structMetalTypeMetalLabel.Location = new System.Drawing.Point(280, 636);
            structMetalTypeMetalLabel.Name = "structMetalTypeMetalLabel";
            structMetalTypeMetalLabel.Size = new System.Drawing.Size(161, 17);
            structMetalTypeMetalLabel.TabIndex = 30;
            structMetalTypeMetalLabel.Text = "Struct Metal Type Metal:";
            // 
            // structMetalFormeLabel
            // 
            structMetalFormeLabel.AutoSize = true;
            structMetalFormeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            structMetalFormeLabel.Location = new System.Drawing.Point(280, 662);
            structMetalFormeLabel.Name = "structMetalFormeLabel";
            structMetalFormeLabel.Size = new System.Drawing.Size(131, 17);
            structMetalFormeLabel.TabIndex = 32;
            structMetalFormeLabel.Text = "Struct Metal Forme:";
            // 
            // structMetalDimensionLabel
            // 
            structMetalDimensionLabel.AutoSize = true;
            structMetalDimensionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            structMetalDimensionLabel.Location = new System.Drawing.Point(280, 688);
            structMetalDimensionLabel.Name = "structMetalDimensionLabel";
            structMetalDimensionLabel.Size = new System.Drawing.Size(157, 17);
            structMetalDimensionLabel.TabIndex = 34;
            structMetalDimensionLabel.Text = "Struct Metal Dimension:";
            // 
            // structMetalTypeAlliageIDLabel
            // 
            structMetalTypeAlliageIDLabel.AutoSize = true;
            structMetalTypeAlliageIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            structMetalTypeAlliageIDLabel.Location = new System.Drawing.Point(280, 714);
            structMetalTypeAlliageIDLabel.Name = "structMetalTypeAlliageIDLabel";
            structMetalTypeAlliageIDLabel.Size = new System.Drawing.Size(186, 17);
            structMetalTypeAlliageIDLabel.TabIndex = 36;
            structMetalTypeAlliageIDLabel.Text = "Struct Metal Type Alliage ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(219, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estimation des coûts";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.bindingSource1;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 726);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(738, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "TableEstimation";
            this.bindingSource1.DataSource = this.bD_Estimation_CoutsDataSet;
            this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // bD_Estimation_CoutsDataSet
            // 
            this.bD_Estimation_CoutsDataSet.DataSetName = "BD_Estimation_CoutsDataSet";
            this.bD_Estimation_CoutsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(37, 22);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Nombre total d\'éléments";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Placer en premier";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Déplacer vers le haut";
            this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Position actuelle";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Déplacer vers le bas";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Placer en dernier";
            this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Ajouter nouveau";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Supprimer";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // textID
            // 
            this.textID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ID", true));
            this.textID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textID.Location = new System.Drawing.Point(428, 265);
            this.textID.Name = "textID";
            this.textID.ReadOnly = true;
            this.textID.Size = new System.Drawing.Size(104, 23);
            this.textID.TabIndex = 3;
            // 
            // textTypeTissu
            // 
            this.textTypeTissu.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TypeTissu", true));
            this.textTypeTissu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTypeTissu.Location = new System.Drawing.Point(428, 355);
            this.textTypeTissu.Name = "textTypeTissu";
            this.textTypeTissu.Size = new System.Drawing.Size(104, 23);
            this.textTypeTissu.TabIndex = 17;
            this.textTypeTissu.Validating += new System.ComponentModel.CancelEventHandler(this.textTypeTissu_Validating);
            // 
            // checkSiJointRetourne
            // 
            this.checkSiJointRetourne.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.bindingSource1, "SiJointRetourne", true));
            this.checkSiJointRetourne.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkSiJointRetourne.Location = new System.Drawing.Point(428, 474);
            this.checkSiJointRetourne.Name = "checkSiJointRetourne";
            this.checkSiJointRetourne.Size = new System.Drawing.Size(104, 24);
            this.checkSiJointRetourne.TabIndex = 25;
            this.checkSiJointRetourne.UseVisualStyleBackColor = true;
            // 
            // structMetalTypeMetalTextBox
            // 
            this.structMetalTypeMetalTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "StructMetalTypeMetal", true));
            this.structMetalTypeMetalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structMetalTypeMetalTextBox.Location = new System.Drawing.Point(428, 633);
            this.structMetalTypeMetalTextBox.Name = "structMetalTypeMetalTextBox";
            this.structMetalTypeMetalTextBox.Size = new System.Drawing.Size(104, 23);
            this.structMetalTypeMetalTextBox.TabIndex = 31;
            // 
            // structMetalFormeTextBox
            // 
            this.structMetalFormeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "StructMetalForme", true));
            this.structMetalFormeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structMetalFormeTextBox.Location = new System.Drawing.Point(428, 659);
            this.structMetalFormeTextBox.Name = "structMetalFormeTextBox";
            this.structMetalFormeTextBox.Size = new System.Drawing.Size(104, 23);
            this.structMetalFormeTextBox.TabIndex = 33;
            // 
            // structMetalDimensionTextBox
            // 
            this.structMetalDimensionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "StructMetalDimension", true));
            this.structMetalDimensionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structMetalDimensionTextBox.Location = new System.Drawing.Point(428, 685);
            this.structMetalDimensionTextBox.Name = "structMetalDimensionTextBox";
            this.structMetalDimensionTextBox.Size = new System.Drawing.Size(104, 23);
            this.structMetalDimensionTextBox.TabIndex = 35;
            // 
            // structMetalTypeAlliageIDTextBox
            // 
            this.structMetalTypeAlliageIDTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "StructMetalTypeAlliageID", true));
            this.structMetalTypeAlliageIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.structMetalTypeAlliageIDTextBox.Location = new System.Drawing.Point(428, 711);
            this.structMetalTypeAlliageIDTextBox.Name = "structMetalTypeAlliageIDTextBox";
            this.structMetalTypeAlliageIDTextBox.Size = new System.Drawing.Size(104, 23);
            this.structMetalTypeAlliageIDTextBox.TabIndex = 37;
            // 
            // comboForme
            // 
            this.comboForme.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Forme", true));
            this.comboForme.DataSource = this.tableFormeBindingSource;
            this.comboForme.DisplayMember = "Forme";
            this.comboForme.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboForme.FormattingEnabled = true;
            this.comboForme.Location = new System.Drawing.Point(428, 294);
            this.comboForme.Name = "comboForme";
            this.comboForme.Size = new System.Drawing.Size(121, 24);
            this.comboForme.TabIndex = 38;
            this.comboForme.ValueMember = "Forme";
            // 
            // tableFormeBindingSource
            // 
            this.tableFormeBindingSource.DataMember = "TableForme";
            this.tableFormeBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_Forme;
            // 
            // bD_Estimation_CoutsDataSet_Forme
            // 
            this.bD_Estimation_CoutsDataSet_Forme.DataSetName = "BD_Estimation_CoutsDataSet_Forme";
            this.bD_Estimation_CoutsDataSet_Forme.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableEstimationTableAdapter
            // 
            this.tableEstimationTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TableEstimationTableAdapter = this.tableEstimationTableAdapter;
            this.tableAdapterManager.UpdateOrder = EstimationCouts.BD_Estimation_CoutsDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // tableFormeTableAdapter
            // 
            this.tableFormeTableAdapter.ClearBeforeFill = true;
            // 
            // buttonDimensions
            // 
            this.buttonDimensions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDimensions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonDimensions.Location = new System.Drawing.Point(555, 288);
            this.buttonDimensions.Name = "buttonDimensions";
            this.buttonDimensions.Size = new System.Drawing.Size(110, 35);
            this.buttonDimensions.TabIndex = 39;
            this.buttonDimensions.Text = "Dimensions";
            this.buttonDimensions.UseVisualStyleBackColor = true;
            this.buttonDimensions.Click += new System.EventHandler(this.buttonDimensions_Click);
            // 
            // labelAvertissement
            // 
            this.labelAvertissement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAvertissement.ForeColor = System.Drawing.Color.Red;
            this.labelAvertissement.Location = new System.Drawing.Point(371, 326);
            this.labelAvertissement.Name = "labelAvertissement";
            this.labelAvertissement.Size = new System.Drawing.Size(299, 23);
            this.labelAvertissement.TabIndex = 40;
            this.labelAvertissement.Text = "NOTE: Faire la forme en plusieurs sections";
            // 
            // buttonQuitter
            // 
            this.buttonQuitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuitter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonQuitter.Location = new System.Drawing.Point(577, 601);
            this.buttonQuitter.Name = "buttonQuitter";
            this.buttonQuitter.Size = new System.Drawing.Size(110, 35);
            this.buttonQuitter.TabIndex = 41;
            this.buttonQuitter.Text = "Quitter";
            this.buttonQuitter.UseVisualStyleBackColor = true;
            this.buttonQuitter.Click += new System.EventHandler(this.buttonQuitter_Click);
            // 
            // comboOrientationTissu
            // 
            this.comboOrientationTissu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboOrientationTissu.DataSource = this.tableOrientationTissuBindingSource;
            this.comboOrientationTissu.DisplayMember = "Description";
            this.comboOrientationTissu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboOrientationTissu.FormattingEnabled = true;
            this.comboOrientationTissu.Location = new System.Drawing.Point(428, 384);
            this.comboOrientationTissu.Name = "comboOrientationTissu";
            this.comboOrientationTissu.Size = new System.Drawing.Size(121, 24);
            this.comboOrientationTissu.TabIndex = 42;
            this.comboOrientationTissu.ValueMember = "ID";
            this.comboOrientationTissu.Validating += new System.ComponentModel.CancelEventHandler(this.comboOrientationTissu_Validating);
            // 
            // tableOrientationTissuBindingSource
            // 
            this.tableOrientationTissuBindingSource.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // bD_Estimation_CoutsDataSet_OrientationTissu
            // 
            this.bD_Estimation_CoutsDataSet_OrientationTissu.DataSetName = "BD_Estimation_CoutsDataSet_OrientationTissu";
            this.bD_Estimation_CoutsDataSet_OrientationTissu.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableOrientationTissuBindingSource5
            // 
            this.tableOrientationTissuBindingSource5.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource5.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // tableOrientationTissuTableAdapter
            // 
            this.tableOrientationTissuTableAdapter.ClearBeforeFill = true;
            // 
            // tableEstimationBindingSource
            // 
            this.tableEstimationBindingSource.DataMember = "TableEstimation";
            this.tableEstimationBindingSource.DataSource = this.bD_Estimation_CoutsDataSet;
            // 
            // tableOrientationTissuBindingSource1
            // 
            this.tableOrientationTissuBindingSource1.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource1.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // bDEstimationCoutsDataSetOrientationTissuBindingSource
            // 
            this.bDEstimationCoutsDataSetOrientationTissuBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            this.bDEstimationCoutsDataSetOrientationTissuBindingSource.Position = 0;
            // 
            // bDEstimationCoutsDataSetOrientationTissuBindingSource1
            // 
            this.bDEstimationCoutsDataSetOrientationTissuBindingSource1.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            this.bDEstimationCoutsDataSetOrientationTissuBindingSource1.Position = 0;
            // 
            // bDEstimationCoutsDataSetBindingSource
            // 
            this.bDEstimationCoutsDataSetBindingSource.DataSource = this.bD_Estimation_CoutsDataSet;
            this.bDEstimationCoutsDataSetBindingSource.Position = 0;
            // 
            // tableOrientationTissuBindingSource2
            // 
            this.tableOrientationTissuBindingSource2.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource2.DataSource = this.bDEstimationCoutsDataSetOrientationTissuBindingSource1;
            // 
            // tableOrientationTissuBindingSource3
            // 
            this.tableOrientationTissuBindingSource3.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource3.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // tableOrientationTissuBindingSource4
            // 
            this.tableOrientationTissuBindingSource4.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource4.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // tableOrientationTissuBindingSource6
            // 
            this.tableOrientationTissuBindingSource6.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource6.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // tableOrientationTissuBindingSource7
            // 
            this.tableOrientationTissuBindingSource7.DataMember = "TableOrientationTissu";
            this.tableOrientationTissuBindingSource7.DataSource = this.bD_Estimation_CoutsDataSet_OrientationTissu;
            // 
            // comboTypeAssemblage
            // 
            this.comboTypeAssemblage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboTypeAssemblage.DataSource = this.tableTypeAssemblageBindingSource;
            this.comboTypeAssemblage.DisplayMember = "Description";
            this.comboTypeAssemblage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypeAssemblage.FormattingEnabled = true;
            this.comboTypeAssemblage.Location = new System.Drawing.Point(428, 414);
            this.comboTypeAssemblage.Name = "comboTypeAssemblage";
            this.comboTypeAssemblage.Size = new System.Drawing.Size(140, 24);
            this.comboTypeAssemblage.TabIndex = 43;
            this.comboTypeAssemblage.ValueMember = "ID";
            this.comboTypeAssemblage.Validating += new System.ComponentModel.CancelEventHandler(this.comboTypeAssemblage_Validating);
            // 
            // tableTypeAssemblageBindingSource
            // 
            this.tableTypeAssemblageBindingSource.DataMember = "TableTypeAssemblage";
            this.tableTypeAssemblageBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_TypeAssemblage;
            // 
            // bD_Estimation_CoutsDataSet_TypeAssemblage
            // 
            this.bD_Estimation_CoutsDataSet_TypeAssemblage.DataSetName = "BD_Estimation_CoutsDataSet_TypeAssemblage";
            this.bD_Estimation_CoutsDataSet_TypeAssemblage.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bDEstimationCoutsDataSetTypeAssemblageBindingSource
            // 
            this.bDEstimationCoutsDataSetTypeAssemblageBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_TypeAssemblage;
            this.bDEstimationCoutsDataSetTypeAssemblageBindingSource.Position = 0;
            // 
            // tableTypeAssemblageTableAdapter
            // 
            this.tableTypeAssemblageTableAdapter.ClearBeforeFill = true;
            // 
            // comboLargeurStrip
            // 
            this.comboLargeurStrip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "LargeurStrip", true));
            this.comboLargeurStrip.DisplayMember = "LargeurStrip";
            this.comboLargeurStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboLargeurStrip.FormattingEnabled = true;
            this.comboLargeurStrip.Location = new System.Drawing.Point(428, 444);
            this.comboLargeurStrip.Name = "comboLargeurStrip";
            this.comboLargeurStrip.Size = new System.Drawing.Size(121, 24);
            this.comboLargeurStrip.TabIndex = 44;
            this.comboLargeurStrip.ValueMember = "LargeurStrip";
            this.comboLargeurStrip.Validating += new System.ComponentModel.CancelEventHandler(this.comboLargeurStrip_Validating);
            // 
            // comboCommentCollerJoint
            // 
            this.comboCommentCollerJoint.DataSource = this.tableCommentCollerJointBindingSource;
            this.comboCommentCollerJoint.DisplayMember = "Description";
            this.comboCommentCollerJoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCommentCollerJoint.FormattingEnabled = true;
            this.comboCommentCollerJoint.Location = new System.Drawing.Point(428, 504);
            this.comboCommentCollerJoint.Name = "comboCommentCollerJoint";
            this.comboCommentCollerJoint.Size = new System.Drawing.Size(121, 24);
            this.comboCommentCollerJoint.TabIndex = 45;
            this.comboCommentCollerJoint.ValueMember = "ID";
            this.comboCommentCollerJoint.Validating += new System.ComponentModel.CancelEventHandler(this.comboCommentCollerJoint_Validating);
            // 
            // bD_Estimation_CoutsDataSet_CommentCollerJoint
            // 
            this.bD_Estimation_CoutsDataSet_CommentCollerJoint.DataSetName = "BD_Estimation_CoutsDataSet_CommentCollerJoint";
            this.bD_Estimation_CoutsDataSet_CommentCollerJoint.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableCommentCollerJointBindingSource
            // 
            this.tableCommentCollerJointBindingSource.DataMember = "TableCommentCollerJoint";
            this.tableCommentCollerJointBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_CommentCollerJoint;
            // 
            // tableCommentCollerJointTableAdapter
            // 
            this.tableCommentCollerJointTableAdapter.ClearBeforeFill = true;
            // 
            // comboCouleurMetal
            // 
            this.comboCouleurMetal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CouleurMetal", true));
            this.comboCouleurMetal.DataSource = this.tableCouleurMetalBindingSource;
            this.comboCouleurMetal.DisplayMember = "Description";
            this.comboCouleurMetal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCouleurMetal.FormattingEnabled = true;
            this.comboCouleurMetal.Location = new System.Drawing.Point(428, 534);
            this.comboCouleurMetal.Name = "comboCouleurMetal";
            this.comboCouleurMetal.Size = new System.Drawing.Size(121, 24);
            this.comboCouleurMetal.TabIndex = 46;
            this.comboCouleurMetal.ValueMember = "Description";
            this.comboCouleurMetal.Validating += new System.ComponentModel.CancelEventHandler(this.comboCouleurMetal_Validating);
            // 
            // bD_Estimation_CoutsDataSet_CouleurMetal
            // 
            this.bD_Estimation_CoutsDataSet_CouleurMetal.DataSetName = "BD_Estimation_CoutsDataSet1";
            this.bD_Estimation_CoutsDataSet_CouleurMetal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bDEstimationCoutsDataSetCouleurMetalBindingSource
            // 
            this.bDEstimationCoutsDataSetCouleurMetalBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_CouleurMetal;
            this.bDEstimationCoutsDataSetCouleurMetalBindingSource.Position = 0;
            // 
            // bDEstimationCoutsDataSetCouleurMetalBindingSource1
            // 
            this.bDEstimationCoutsDataSetCouleurMetalBindingSource1.DataSource = this.bD_Estimation_CoutsDataSet_CouleurMetal;
            this.bDEstimationCoutsDataSetCouleurMetalBindingSource1.Position = 0;
            // 
            // bD_Estimation_CoutsDataSet_CouleurMetal1
            // 
            this.bD_Estimation_CoutsDataSet_CouleurMetal1.DataSetName = "BD_Estimation_CoutsDataSet_CouleurMetal";
            this.bD_Estimation_CoutsDataSet_CouleurMetal1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bDEstimationCoutsDataSetCouleurMetal1BindingSource
            // 
            this.bDEstimationCoutsDataSetCouleurMetal1BindingSource.DataSource = this.bD_Estimation_CoutsDataSet_CouleurMetal1;
            this.bDEstimationCoutsDataSetCouleurMetal1BindingSource.Position = 0;
            // 
            // tableCouleurMetalBindingSource
            // 
            this.tableCouleurMetalBindingSource.DataMember = "TableCouleurMetal";
            this.tableCouleurMetalBindingSource.DataSource = this.bD_Estimation_CoutsDataSet_CouleurMetal1;
            // 
            // tableCouleurMetalTableAdapter
            // 
            this.tableCouleurMetalTableAdapter.ClearBeforeFill = true;
            // 
            // buttonStructureMetallique
            // 
            this.buttonStructureMetallique.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStructureMetallique.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonStructureMetallique.Location = new System.Drawing.Point(428, 564);
            this.buttonStructureMetallique.Name = "buttonStructureMetallique";
            this.buttonStructureMetallique.Size = new System.Drawing.Size(110, 55);
            this.buttonStructureMetallique.TabIndex = 47;
            this.buttonStructureMetallique.Text = "Structure métallique";
            this.buttonStructureMetallique.UseVisualStyleBackColor = true;
            this.buttonStructureMetallique.Click += new System.EventHandler(this.buttonStructureMetallique_Click);
            // 
            // FrmEstimationCouts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(738, 751);
            this.Controls.Add(this.buttonStructureMetallique);
            this.Controls.Add(this.comboCouleurMetal);
            this.Controls.Add(this.comboCommentCollerJoint);
            this.Controls.Add(this.comboLargeurStrip);
            this.Controls.Add(this.comboTypeAssemblage);
            this.Controls.Add(this.comboOrientationTissu);
            this.Controls.Add(this.buttonQuitter);
            this.Controls.Add(this.labelAvertissement);
            this.Controls.Add(this.buttonDimensions);
            this.Controls.Add(this.comboForme);
            this.Controls.Add(iDLabel);
            this.Controls.Add(this.textID);
            this.Controls.Add(formeLabel);
            this.Controls.Add(typeTissuLabel);
            this.Controls.Add(this.textTypeTissu);
            this.Controls.Add(orientationTissuIDLabel);
            this.Controls.Add(typeAssemblageIDLabel);
            this.Controls.Add(largeurStripLabel);
            this.Controls.Add(siJointRetourneLabel);
            this.Controls.Add(this.checkSiJointRetourne);
            this.Controls.Add(commentCollerJointIDLabel);
            this.Controls.Add(couleurMetalLabel);
            this.Controls.Add(structMetalTypeMetalLabel);
            this.Controls.Add(this.structMetalTypeMetalTextBox);
            this.Controls.Add(structMetalFormeLabel);
            this.Controls.Add(this.structMetalFormeTextBox);
            this.Controls.Add(structMetalDimensionLabel);
            this.Controls.Add(this.structMetalDimensionTextBox);
            this.Controls.Add(structMetalTypeAlliageIDLabel);
            this.Controls.Add(this.structMetalTypeAlliageIDTextBox);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.label1);
            this.Name = "FrmEstimationCouts";
            this.Text = "Estimation des coûts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEstimationCouts_FormClosing);
            this.Load += new System.EventHandler(this.FrmEstimationCouts_Load);
            this.Shown += new System.EventHandler(this.FrmEstimationCouts_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableFormeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_Forme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_OrientationTissu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableEstimationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetOrientationTissuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetOrientationTissuBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOrientationTissuBindingSource7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableTypeAssemblageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_TypeAssemblage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetTypeAssemblageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_CommentCollerJoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableCommentCollerJointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_CouleurMetal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetCouleurMetalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetCouleurMetalBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bD_Estimation_CoutsDataSet_CouleurMetal1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDEstimationCoutsDataSetCouleurMetal1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableCouleurMetalBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private BD_Estimation_CoutsDataSet bD_Estimation_CoutsDataSet;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private BD_Estimation_CoutsDataSetTableAdapters.TableEstimationTableAdapter tableEstimationTableAdapter;
        private BD_Estimation_CoutsDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox textID;
        private System.Windows.Forms.TextBox textTypeTissu;
        private System.Windows.Forms.CheckBox checkSiJointRetourne;
        private System.Windows.Forms.TextBox structMetalTypeMetalTextBox;
        private System.Windows.Forms.TextBox structMetalFormeTextBox;
        private System.Windows.Forms.TextBox structMetalDimensionTextBox;
        private System.Windows.Forms.TextBox structMetalTypeAlliageIDTextBox;
        private System.Windows.Forms.ComboBox comboForme;
        private BD_Estimation_CoutsDataSet_Forme bD_Estimation_CoutsDataSet_Forme;
        private System.Windows.Forms.BindingSource tableFormeBindingSource;
        private BD_Estimation_CoutsDataSet_FormeTableAdapters.TableFormeTableAdapter tableFormeTableAdapter;
        private System.Windows.Forms.Button buttonDimensions;
        private System.Windows.Forms.Label labelAvertissement;
        private System.Windows.Forms.Button buttonQuitter;
        private System.Windows.Forms.ComboBox comboOrientationTissu;
        private BD_Estimation_CoutsDataSet_OrientationTissu bD_Estimation_CoutsDataSet_OrientationTissu;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource;
        private BD_Estimation_CoutsDataSet_OrientationTissuTableAdapters.TableOrientationTissuTableAdapter tableOrientationTissuTableAdapter;
        private System.Windows.Forms.BindingSource tableEstimationBindingSource;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource1;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource2;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetOrientationTissuBindingSource1;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetOrientationTissuBindingSource;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetBindingSource;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource3;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource4;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource5;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource6;
        private System.Windows.Forms.BindingSource tableOrientationTissuBindingSource7;
        private System.Windows.Forms.ComboBox comboTypeAssemblage;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetTypeAssemblageBindingSource;
        private BD_Estimation_CoutsDataSet_TypeAssemblage bD_Estimation_CoutsDataSet_TypeAssemblage;
        private System.Windows.Forms.BindingSource tableTypeAssemblageBindingSource;
        private BD_Estimation_CoutsDataSet_TypeAssemblageTableAdapters.TableTypeAssemblageTableAdapter tableTypeAssemblageTableAdapter;
        private System.Windows.Forms.ComboBox comboLargeurStrip;
        private System.Windows.Forms.ComboBox comboCommentCollerJoint;
        private BD_Estimation_CoutsDataSet_CommentCollerJoint bD_Estimation_CoutsDataSet_CommentCollerJoint;
        private System.Windows.Forms.BindingSource tableCommentCollerJointBindingSource;
        private BD_Estimation_CoutsDataSet_CommentCollerJointTableAdapters.TableCommentCollerJointTableAdapter tableCommentCollerJointTableAdapter;
        private System.Windows.Forms.ComboBox comboCouleurMetal;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetCouleurMetal1BindingSource;
        private BD_Estimation_CoutsDataSet_CouleurMetal bD_Estimation_CoutsDataSet_CouleurMetal1;
        private BD_Estimation_CoutsDataSet_CouleurMetal bD_Estimation_CoutsDataSet_CouleurMetal;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetCouleurMetalBindingSource;
        private System.Windows.Forms.BindingSource bDEstimationCoutsDataSetCouleurMetalBindingSource1;
        private System.Windows.Forms.BindingSource tableCouleurMetalBindingSource;
        private BD_Estimation_CoutsDataSet_CouleurMetalTableAdapters.TableCouleurMetalTableAdapter tableCouleurMetalTableAdapter;
        private System.Windows.Forms.Button buttonStructureMetallique;
    }
}

