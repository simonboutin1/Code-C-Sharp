using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace EstimationCouts
{
    public static class GeneralTools
    {
        //Align the form to the center of the screen
        public static void ReallyCenterToScreen(Form form)
        {
            Screen screen = Screen.FromControl(form);

            Rectangle workingArea = screen.WorkingArea;
            form.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - form.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - form.Height) / 2)
            };
        }

        //Accept a float regardless the decimal separator is "." or "," 
        public static float ifFloatValue(string strValue)
        {
            float floatValue;

            //Allow to accept either "." or ","
            bool success1 = float.TryParse(strValue.Replace(",", "."), out floatValue);
            bool success2 = float.TryParse(strValue.Replace(".", ","), out floatValue);

            if (!(success1 || success2)) floatValue = float.MinValue;

            return floatValue;
        }

        //Message box used on invalid field value
        public static void MsgBoxChampInvalide(string message, Control control)
        {
            MessageBox.Show(message, "Champ invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            control.Focus();
        }

        //Trim the value of a control
        public static void TrimControl(Control control, int size)
        {
            string value = control.Text.Trim();
                
            value = value.Substring(0, Math.Min(value.Length, size)).Trim();

            if (control.Text != value) control.Text = value;
        }

        //Perform the validation of null autorized database ID linked comboBox
        public static void combo_ID_Null_Validating(SqlConnection DBconnection,
                                                    DataRow currentRow,
                                                    BindingSource bindingSource,
                                                    Form form,
                                                    string name,
                                                    bool flagValidateFormCalled,
                                                    ref bool flagInvalidField,
                                                    CancelEventArgs e)
        {
            if (!flagValidateFormCalled)
            {
                bool ifPassValidation = true;

                if (!(string.IsNullOrEmpty(form.Controls["combo" + name].Text)))
                {
                    if (((ComboBox)form.Controls["combo" + name]).SelectedValue == null)
                    {
                        ifPassValidation = false;
                        GeneralTools.MsgBoxChampInvalide("Vous devez choisir un élément de la liste", form.Controls["combo" + name]);
                    }
                }

                flagInvalidField = !ifPassValidation;

                if (ifPassValidation)
                    SetFieldCombo_ID(DBconnection,
                                     currentRow,
                                     bindingSource,
                                     form,
                                     name);
                else e.Cancel = true;
            }
        }

        //Essential if a database linked comboBox must store a value but display another value
        //Set the appropriate value in the Table_x.y_ID field according to the comboBox.Text value
        private static void SetFieldCombo_ID(SqlConnection DBconnection,
                                             DataRow currentRow,
                                             BindingSource bindingSource,
                                             Form form,
                                             string name)
        {
            if (currentRow != null)
            {
                string SQL = "SELECT ID FROM Table" + name + " WHERE Description = @Description";

                SqlCommand SQLCommand = new SqlCommand(SQL, DBconnection);

                SQLCommand.Parameters.Add("Description", SqlDbType.VarChar).Value = form.Controls["combo" + name].Text;

                Int16? value = (Int16?)SQLCommand.ExecuteScalar();

                if (value == null) currentRow[name + "ID"] = DBNull.Value;
                else currentRow[name + "ID"] = value;

                bindingSource.EndEdit();
            }
        }

        //Essential if a database linked comboBox must store a value but display another value
        //Get the appropriate value for the comboBox.Text value according to the Table_x.y_ID field
        public static void GetFieldCombo_ID(SqlConnection DBconnection,
                                            DataRow currentRow,
                                            Form form,
                                            string name)
        {
            if (currentRow != null)
            {
                string SQL = "SELECT Description FROM Table" + name + " WHERE ID = @ID";

                SqlCommand SQLCommand = new SqlCommand(SQL, DBconnection);

                SQLCommand.Parameters.Add("ID", SqlDbType.SmallInt).Value = currentRow[name + "ID"];

                string value = (string)SQLCommand.ExecuteScalar();

                form.Controls["combo" + name].Text = value;
            }
        }
    }
}
