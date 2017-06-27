using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace EstimationCouts
{
    public partial class FrmDimensionsForme : Form
    {
        private FrmEstimationCouts parent;
        private string id_Estimation;
        private string forme;

        public FrmDimensionsForme(FrmEstimationCouts parent, DataSet dataSet, string id_Estimation)
        {
            InitializeComponent();

            this.parent = parent;
            this.id_Estimation = id_Estimation;

            //Allow to make the link with the current record of the parent form
            this.tableEstimationBindingSource.DataSource = dataSet;
            this.tableEstimationBindingSource.Filter = "ID = " + id_Estimation;

            forme = parent.GetForme();
        }

        private void FrmDimensionsForme_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'bD_Estimation_CoutsDataSet.TableEstimation'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.tableEstimationTableAdapter.Fill(this.bD_Estimation_CoutsDataSet.TableEstimation);

            //Align the form to the center of the screen
            GeneralTools.ReallyCenterToScreen(this);
        }

        private void FrmDimensionsForme_Shown(object sender, EventArgs e)
        {
            labelTitre.Text = forme;

            AdjustFields();
        }

        //Display the appropriate labels based on the "forme"
        private void AdjustFields()
        {
            List<string> listeForme = parent.GetListeForme().ConvertAll(d => d.ToUpper()); ;
            List<string> labelDimension = new List<string>();
            string formeUpper = forme.ToUpper();

            if (formeUpper == listeForme[0])
            {
                //Rond
                labelDimension.Add("Diamètre du haut:");
                labelDimension.Add("Diamètre du bas:");
                labelDimension.Add("Hauteur:");
            }
            else if (formeUpper == listeForme[1])
            {
                //Carré
                labelDimension.Add("Longueur du haut:");
                labelDimension.Add("Largeur du haut:");
                labelDimension.Add("Longueur du bas:");
                labelDimension.Add("Largeur du bas:");
                labelDimension.Add("Hauteur:");
            }
            else if (formeUpper == listeForme[2])
            {
                //Rectangle
                labelDimension.Add("Petit côté du haut:");
                labelDimension.Add("Long côté du haut:");
                labelDimension.Add("Petit côté du bas:");
                labelDimension.Add("Long côté du bas:");
                labelDimension.Add("Hauteur:");
            }
            else if (formeUpper == listeForme[3])
            {
                //Ovale
                labelDimension.Add("Longueur du haut:");
                labelDimension.Add("Largeur du haut:");
                labelDimension.Add("Longueur du bas:");
                labelDimension.Add("Largeur du bas:");
                labelDimension.Add("Hauteur:");
            }
            else if (formeUpper == listeForme[4])
            {
                //Demi lune
                labelDimension.Add("Profondeur du haut:");
                labelDimension.Add("Largeur du haut:");
                labelDimension.Add("Profondeur du bas:");
                labelDimension.Add("Largeur du bas:");
                labelDimension.Add("Hauteur:");
            }
            else if (formeUpper == listeForme[5])
            {
                //Demi rond
                labelDimension.Add("Profondeur du haut:");
                labelDimension.Add("Largeur du haut:");
                labelDimension.Add("Profondeur du bas:");
                labelDimension.Add("Largeur du bas:");
                labelDimension.Add("Hauteur:");
            }
            else if (formeUpper == listeForme[6])
            {
                //Demi rectangle
                labelDimension.Add("Profondeur du haut:");
                labelDimension.Add("Largeur du haut:");
                labelDimension.Add("Profondeur du bas:");
                labelDimension.Add("Largeur du bas:");
                labelDimension.Add("Hauteur:");
            }
            else
            {
                labelDimension.Add("Dimension 1:");
                labelDimension.Add("Dimension 2:");
                labelDimension.Add("Dimension 3:");
                labelDimension.Add("Dimension 4:");
                labelDimension.Add("Dimension 5:");
            }

            string noField;

            for (int i = 0; i < labelDimension.Count; i++)
            {
                noField = (i + 1).ToString();

                Controls["labelDimension" + noField].Text = labelDimension[i];
                Controls["labelDimension" + noField].Visible = true;
                Controls["textDimension" + noField].Visible = true;

                //Essential to be able to save the null values
                Controls["textDimension" + noField].DataBindings["Text"].NullValue = "";
            }
        }

        private void buttonFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Perform the validation of a textBox
        private void ValidateField(TextBox textDimension)
        {
            textDimension.Text = textDimension.Text.Trim();

            string strValue = textDimension.Text;

            if (!(string.IsNullOrEmpty(strValue)))
            {
                float floatValue = GeneralTools.ifFloatValue(strValue);

                //If it's not a float
                if (floatValue == float.MinValue)
                {
                    GeneralTools.MsgBoxChampInvalide("La valeur doit être un nombre réel", textDimension);
                }
                else if (floatValue < 3 || floatValue > 240)
                {
                    GeneralTools.MsgBoxChampInvalide("La valeur doit être située entre 3 et 240", textDimension);
                }
                else
                {
                    //Essential to accept both "." or "," as decimal separator
                    textDimension.Text = floatValue.ToString("0.##");
                }
            }
        }

        private void textDimension1_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(textDimension1);
        }

        private void textDimension2_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(textDimension2);
        }

        private void textDimension3_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(textDimension3);
        }

        private void textDimension4_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(textDimension4);
        }

        private void textDimension5_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(textDimension5);
        }

        private void FrmDimensionsForme_FormClosing(object sender, FormClosingEventArgs e)
        {
            //To be sure all values will be saved
            buttonFermer.Focus();
        }
    }
}
