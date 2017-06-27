using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstimationCouts
{
    public partial class FrmStructureMetallique : Form
    {
        private FrmEstimationCouts parent;
        private string id_Estimation;
        private List<string> listeTypeMetal;

        public FrmStructureMetallique(FrmEstimationCouts parent, DataSet dataSet, string id_Estimation)
        {
            InitializeComponent();

            this.parent = parent;
            this.id_Estimation = id_Estimation;

            //Allow to make the link with the current record of the parent form
            this.tableEstimationBindingSource.DataSource = dataSet;
            this.tableEstimationBindingSource.Filter = "ID = " + id_Estimation;
        }

        private void FrmStructureMetallique_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet_TypeMetal.TableStructMetalTypeMetal'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableStructMetalTypeMetalTableAdapter.Fill(this.bD_Estimation_CoutsDataSet_TypeMetal.TableStructMetalTypeMetal);
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet.TableEstimation'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableEstimationTableAdapter.Fill(this.bD_Estimation_CoutsDataSet.TableEstimation);

            //Align the form to the center of the screen
            GeneralTools.ReallyCenterToScreen(this);

            //To detect value changes in comboForme
            comboTypeMetal.SelectedValueChanged += new EventHandler(comboTypeMetalValueChanged);
            comboTypeMetal.Validating += new CancelEventHandler(comboTypeMetalValueChanged);

            //To detect value changes in comboForme
            comboForme.SelectedValueChanged += new EventHandler(comboFormeValueChanged);
            comboForme.Validating += new CancelEventHandler(comboFormeValueChanged);

            listeTypeMetal = GetListeTypeMetal().ConvertAll(d => d.ToUpper());

            //Essential to be able to save the null values
            Controls["comboTypeMetal"].DataBindings["Text"].NullValue = "";

            if (string.IsNullOrEmpty(comboTypeMetal.Text))
            {
                comboTypeMetal.SelectedIndex = 1;
            }
        }

        private List<string> GetListeTypeMetal()
        {
            List<string> listeTypeMetal = new List<string>();

            for (int i = 0; i < comboTypeMetal.Items.Count; i++)
            {
                listeTypeMetal.Add(comboTypeMetal.GetItemText(comboTypeMetal.Items[i]));
            }

            return listeTypeMetal;
        }

        private void buttonFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmStructureMetallique_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ValidateChildren())
            {
                //To be sure all values will be saved
                buttonFermer.Focus();
            }
            else e.Cancel = true;
        }

        private void comboTypeMetalValueChanged(object sender, EventArgs e)
        {
            GeneralTools.TrimControl(comboTypeMetal, 20);

            bool ifPassValidation = true;

            if (string.IsNullOrEmpty(comboTypeMetal.Text))
            {
                ifPassValidation = false;
                GeneralTools.MsgBoxChampInvalide("Vous devez entrer une valeur", comboTypeMetal);
            }
            else
            {
                FillComboForme();
            }

            if (!ifPassValidation) ((CancelEventArgs)e).Cancel = true;
        }

        //Fill the list of comboForme
        private void FillComboForme()
        {
            List<Forme> forme = new List<Forme>();

            string typeMetalUpper = comboTypeMetal.Text.ToUpper();

            if (typeMetalUpper == listeTypeMetal[0] || typeMetalUpper == listeTypeMetal[1])
            {
                //Tige ou Tube
                forme.Add(new Forme("Rond"));
                forme.Add(new Forme("Carré"));
            }
            else if (typeMetalUpper == listeTypeMetal[2])
            {
                //Profilé
                forme.Add(new Forme("Carré"));
            }

            comboForme.DataSource = forme;
            comboForme.ValueMember = "Description";
            comboForme.DisplayMember = "Description";
            
            //Essential to be able to save the null values
            Controls["comboForme"].DataBindings["Text"].NullValue = "";

            comboForme.SelectedValueChanged -= new EventHandler(comboFormeValueChanged);
            comboForme.Validating -= new CancelEventHandler(comboFormeValueChanged);
            comboForme.Text = String.Empty;
            comboForme.SelectedValueChanged += new EventHandler(comboFormeValueChanged);
            comboForme.Validating += new CancelEventHandler(comboFormeValueChanged);
        }

        private void comboFormeValueChanged(object sender, EventArgs e)
        {
            bool ifPassValidation = true;

            string typeMetalUpper = comboTypeMetal.Text.ToUpper();

            if (typeMetalUpper == listeTypeMetal[0] || typeMetalUpper == listeTypeMetal[1] || typeMetalUpper == listeTypeMetal[2])
            {
                //Tige, Tube ou Profilé

                //If comboForm is null or the item isn't in its list
                if (string.IsNullOrEmpty(comboForme.Text) || comboForme.SelectedValue == null)
                {
                    ifPassValidation = false;
                    GeneralTools.MsgBoxChampInvalide("Vous devez choisir un élément de la liste", comboForme);
                }
            }

            if (!ifPassValidation) ((CancelEventArgs)e).Cancel = true;
        }
    }

    public class Forme
    {
        public string Description { get; set; }

        public Forme(string description)
        {
            Description = description;
        }
    }
}
