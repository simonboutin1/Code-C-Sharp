using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace EstimationCouts
{
    public partial class FrmEstimationCouts : Form
    {
        SqlConnection DBconnection;
        DataRow currentRow;

        bool flagValidateFormCalled = false; //Flag to avoid displaying validation messages twice when changing current record
        bool flagInvalidField = false;       //Flag to avoid displaying validation messages twice when closing the form

        public FrmEstimationCouts()
        {
            InitializeComponent();

            DBconnection = new SqlConnection();
            DBconnection.ConnectionString = "Data Source = LOGTI033826\\SQLEXPRESS; Initial Catalog =\"BD Estimation Couts\";Integrated Security=True";

            try
            {
                DBconnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source");
            }

            //Essential if the form open with no record
            CurrentRow = GetCurrentRow();
        }

        //Destructor of the class
        ~FrmEstimationCouts()
        {
            DBconnection.Close();
        }

        //When currentRow = null, all controls will be invisible
        public DataRow CurrentRow
        {
            get { return currentRow; }

            set
            {
                currentRow = value;

                foreach (Control c in this.Controls)
                {
                    c.Visible = currentRow == null ? false : true;
                }

                bindingNavigator1.Visible = true;
            }
        }

        private void FrmEstimationCouts_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet_CouleurMetal1.TableCouleurMetal'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableCouleurMetalTableAdapter.Fill(this.bD_Estimation_CoutsDataSet_CouleurMetal1.TableCouleurMetal);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet_CommentCollerJoint.TableCommentCollerJoint'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableCommentCollerJointTableAdapter.Fill(this.bD_Estimation_CoutsDataSet_CommentCollerJoint.TableCommentCollerJoint);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet_TypeAssemblage.TableTypeAssemblage'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableTypeAssemblageTableAdapter.Fill(this.bD_Estimation_CoutsDataSet_TypeAssemblage.TableTypeAssemblage);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet_OrientationTissu.TableOrientationTissu'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableOrientationTissuTableAdapter.Fill(this.bD_Estimation_CoutsDataSet_OrientationTissu.TableOrientationTissu);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet_Forme.TableForme'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableFormeTableAdapter.Fill(this.bD_Estimation_CoutsDataSet_Forme.TableForme);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet.TableEstimation'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableEstimationTableAdapter.Fill(this.bD_Estimation_CoutsDataSet.TableEstimation);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet.TableEstimation'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableEstimationTableAdapter.Fill(this.bD_Estimation_CoutsDataSet.TableEstimation);
            
            //Set the icon at the left corner of the form
            this.Icon = new Icon (Assembly.GetExecutingAssembly().GetManifestResourceStream("EstimationCouts.Resources.logo.ico"));

            //Align the form to the center of the screen
            GeneralTools.ReallyCenterToScreen(this);

            //To detect value changes in comboForme
            comboForme.SelectedValueChanged += new EventHandler(comboFormeValueChanged);
            comboForme.Validating += new CancelEventHandler(comboFormeValueChanged);

            //Essential to be able to save the null values
            Controls["comboForme"].DataBindings["Text"].NullValue = "";
            Controls["textTypeTissu"].DataBindings["Text"].NullValue = "";
            Controls["comboOrientationTissu"].DataBindings["Text"].NullValue = "";
            Controls["comboTypeAssemblage"].DataBindings["Text"].NullValue = "";
            Controls["comboCommentCollerJoint"].DataBindings["Text"].NullValue = "";
        }

        //Perform the validations of the form
        private bool ValidateForm()
        {
            //The current control must lost its focus to commit its change before the "ValidateChildren()" call
            flagValidateFormCalled = true;
            textID.Focus();
            flagValidateFormCalled = false;

            //Force to apply validation on each control of the form
            if (ValidateChildren())
            {
                SaveData();
                return true;
            }
            else return false;
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Position > 0)
            {
                if (ValidateForm()) bindingSource1.MoveFirst();
            }
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Position > 0)
            {
                if (ValidateForm()) bindingSource1.MovePrevious();
            }
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Position < bindingSource1.Count - 1)
            {
                if (ValidateForm()) bindingSource1.MoveNext();
            }
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Position < bindingSource1.Count - 1)
            {
                if (ValidateForm()) bindingSource1.MoveLast();
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                bindingSource1.AddNew();

                if (checkSiJointRetourne.CheckState == CheckState.Indeterminate)
                {
                    checkSiJointRetourne.Checked = false;
                }
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Êtes-vous certain de vouloir supprimer cet enregistrement ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                if (this.BindingContext[bindingSource1].Count == 1) CurrentRow = null;

                bindingSource1.RemoveCurrent();
                ValidateForm();
            }
        }

        private void FrmEstimationCouts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagInvalidField) e.Cancel = true;
            else
            {
                if (ValidateForm())
                {
                    Application.Exit();
                }
                else e.Cancel = true;
            }
        }

        //Save the modified dataset to the database
        private void SaveData()
        {
            this.Validate();
            this.bindingSource1.EndEdit();

            try
            {
                this.tableEstimationTableAdapter.Update(this.bD_Estimation_CoutsDataSet.TableEstimation);
                
            }
            catch (DBConcurrencyException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void buttonDimensions_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (string.IsNullOrEmpty(comboForme.Text.Trim()))
                {
                    GeneralTools.MsgBoxChampInvalide("Vous devez entrer une forme", comboForme);
                }
                else
                {
                    SaveData();

                    FrmDimensionsForme dialog = new FrmDimensionsForme(this, this.bD_Estimation_CoutsDataSet, this.textID.Text);

                    dialog.ShowDialog(this);

                    //Refresh the value in all controls
                    bindingSource1.ResetBindings(false);
                }
            }
        }

        public string GetForme()
        {
            return (CurrentRow == null) ? String.Empty : CurrentRow["Forme"].ToString();
        }

        public List<string> GetListeForme()
        {
            List<string> listeForme = new List<string>();

            for (int i = 0; i < comboForme.Items.Count; i++)
            {
                listeForme.Add(comboForme.GetItemText(comboForme.Items[i]));
            }

            return listeForme;
        }

        private DataRow GetCurrentRow()
        {
            int position = this.BindingContext[bindingSource1].Position;

            if (position > -1)
            {
                CurrentRow = (DataRow)((DataRowView)this.bindingSource1.Current).Row;
            }
            else CurrentRow = null;

            return CurrentRow;
        }

        //Triggered when the current database record position change 
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            CurrentRow = GetCurrentRow();

            if (CurrentRow != null)
            {
                GetFieldCombo_ID("OrientationTissu");
                GetFieldCombo_ID("TypeAssemblage");
                GetFieldCombo_ID("CommentCollerJoint");

                FillComboLargeurStrip();

                displayWarning();
            }
        }

        //Essential if a database linked comboBox must store a value but display another value
        //Get the appropriate value for the comboBox.Text value according to the Table_x.y_ID field
        private void GetFieldCombo_ID(string name)
        {
            GeneralTools.GetFieldCombo_ID(DBconnection,
                                          CurrentRow,
                                          this,
                                          name);
        }

        //Fill the list of comboLargeurStrip
        private void FillComboLargeurStrip()
        {
            string sql = "";

            sql += "SELECT LargeurStrip FROM TableEstimation";
            sql += " WHERE LargeurStrip IS NOT NULL";
            sql += " GROUP BY LargeurStrip ORDER BY LargeurStrip";

            SqlCommand SQLCommand = new SqlCommand(sql, DBconnection);
            SqlDataAdapter da = new SqlDataAdapter(SQLCommand);
            DataTable dt = new DataTable();

            da.Fill(dt);

            comboLargeurStrip.DataSource = dt;
            comboLargeurStrip.ValueMember = "LargeurStrip";
            comboLargeurStrip.DisplayMember = "LargeurStrip";

            //To display the current value after the update of the Datasource
            comboLargeurStrip.Text = currentRow["LargeurStrip"].ToString();
            
            //To allow to save null after the update of the Datasource
            Controls["comboLargeurStrip"].DataBindings["Text"].NullValue = "";
        }

        //Display a warning if a dimension is greater than 84
        private void displayWarning()
        {
            string forme = GetForme().ToUpper();
            List<string> listeForme = GetListeForme().ConvertAll(d => d.ToUpper());

            bool ifDisplay = false;

            if (!(string.IsNullOrEmpty(forme)) && CurrentRow != null)
            {
                List<float> values = new List<float>();

                if (forme == listeForme[0])
                {
                    //Rond
                    values.Add(currentRow.IsNull("Dimension1") ? 0 : float.Parse(currentRow["Dimension1"].ToString()));
                    values.Add(currentRow.IsNull("Dimension2") ? 0 : float.Parse(currentRow["Dimension2"].ToString()));
                    values.Add(currentRow.IsNull("Dimension3") ? 0 : float.Parse(currentRow["Dimension3"].ToString()));
                }
                else
                {
                    values.Add(currentRow.IsNull("Dimension1") ? 0 : float.Parse(currentRow["Dimension1"].ToString()));
                    values.Add(currentRow.IsNull("Dimension2") ? 0 : float.Parse(currentRow["Dimension2"].ToString()));
                    values.Add(currentRow.IsNull("Dimension3") ? 0 : float.Parse(currentRow["Dimension3"].ToString()));
                    values.Add(currentRow.IsNull("Dimension4") ? 0 : float.Parse(currentRow["Dimension4"].ToString()));
                    values.Add(currentRow.IsNull("Dimension5") ? 0 : float.Parse(currentRow["Dimension5"].ToString()));
                }

                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] > 84)
                    {
                        ifDisplay = true;
                        break;
                    }
                }
            }

            labelAvertissement.Visible = ifDisplay;
        }

        private void FrmEstimationCouts_Shown(object sender, EventArgs e)
        {
            displayWarning();
        }

        private void buttonQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboFormeValueChanged(object sender, EventArgs e)
        {
            GeneralTools.TrimControl(comboForme, 20);

            if (!flagValidateFormCalled)
            {
                //Essential to force to commit the changes before displaying the warning
                this.bindingSource1.EndEdit();

                displayWarning();
            }
        }

        private void textTypeTissu_Validating(object sender, CancelEventArgs e)
        {
            GeneralTools.TrimControl(textTypeTissu, 20);
        }

        private void comboOrientationTissu_Validating(object sender, CancelEventArgs e)
        {
            GeneralTools.TrimControl(comboOrientationTissu, 15);

            combo_ID_Null_Validating("OrientationTissu", e);
        }

        private void comboTypeAssemblage_Validating(object sender, CancelEventArgs e)
        {
            GeneralTools.TrimControl(comboTypeAssemblage, 25);

            combo_ID_Null_Validating("TypeAssemblage", e);
        }

        private void comboCommentCollerJoint_Validating(object sender, CancelEventArgs e)
        {
            GeneralTools.TrimControl(comboCommentCollerJoint, 20);

            combo_ID_Null_Validating("CommentCollerJoint", e);
        }

        //Perform the validation of null autorized database ID linked comboBox
        private void combo_ID_Null_Validating(string name, CancelEventArgs e)
        {
            GeneralTools.combo_ID_Null_Validating(DBconnection,
                                                  CurrentRow,
                                                  bindingSource1,
                                                  this,
                                                  name,
                                                  flagValidateFormCalled,
                                                  ref flagInvalidField,
                                                  e);
        }

        private void comboLargeurStrip_Validating(object sender, CancelEventArgs e)
        {
            GeneralTools.TrimControl(comboLargeurStrip, 5);

            if (!flagValidateFormCalled)
            {
                bool ifPassValidation = true;

                if (!(string.IsNullOrEmpty(comboLargeurStrip.Text)))
                {
                    string pattern = "^([1-9]{1})/([1-9]{1})$"; //Ex: "1/9", "8/2", "4/3"

                    if (!Regex.IsMatch(comboLargeurStrip.Text, pattern))
                    {
                        ifPassValidation = false;
                        GeneralTools.MsgBoxChampInvalide("Vous devez utiliser ce format: 9/9", comboLargeurStrip);
                    }
                }

                flagInvalidField = !ifPassValidation;

                if (!ifPassValidation) e.Cancel = true;
            }
        }

        private void comboCouleurMetal_Validating(object sender, CancelEventArgs e)
        {
            GeneralTools.TrimControl(comboCouleurMetal, 20);
        }

        private void buttonStructureMetallique_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveData();

                FrmStructureMetallique dialog = new FrmStructureMetallique(this, this.bD_Estimation_CoutsDataSet, this.textID.Text);

                dialog.ShowDialog(this);

                //Refresh the value in all controls
                bindingSource1.ResetBindings(false);
            }
        }
    }
}