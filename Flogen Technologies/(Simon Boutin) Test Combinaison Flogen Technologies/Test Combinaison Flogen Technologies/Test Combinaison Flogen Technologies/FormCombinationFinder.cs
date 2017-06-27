using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq.Expressions;
using System.Windows.Forms;
using GeneralTools;

//This class is the only form of the application
namespace Test_Combinaison_Flogen_Technologies
{
    public partial class FormCombinationFinder : Form
    {
        //NOTE: The directory "\Resources\..." must be copied in the same directory as the .exe

        //NOTE: On the properties window of the ressource text file, set the "Build Action" property to "None" since I must edit it.
        //      Set also the "Copy To Output Directory" to "Copy if newer"
        //NOTE: If I only need to read it, set the "Build Action" property to "Embedded Resource"
        private string valuesFilePath;

        private string tableCombinationName;
        private string tableDataName;
        private string InputPrefix;
        private string OutputPrefix;

        private int noCombination;

        OleDbConnection connection;
        DatabaseValidation dbValidation;

        public FormCombinationFinder()
        {
            InitializeComponent();

            valuesFilePath = GetPathDefaultValues();

            chartCombination.Visible = false;
        }

        //When the "Find next combination" button is clicked
        private void buttonFindCombination_Click(object sender, EventArgs e)
        {
            List<string> inputColumnsName = dbValidation.GetInputColumnsName();
            List<string> outputColumnsName = dbValidation.GetOutputColumnsName();

            DataRepository dataRepository = new DataRepository(connection,
                                                               tableCombinationName,
                                                               tableDataName,
                                                               inputColumnsName,
                                                               outputColumnsName);

            dataRepository.FillDtCombination();
            dataRepository.FillDtData();

            SearchTool searchTool = new SearchTool(dataRepository.GetDtData(), inputColumnsName);

            if (noCombination >= dataRepository.GetDtCombination().Rows.Count) noCombination = 0;

            DataRow inputCombinationRow = dataRepository.GetDtCombination().Rows[noCombination];

            textFindCombination.Text = GetStrCombination(inputColumnsName,
                                                         inputCombinationRow);

            int combinationIndex = searchTool.GetCombinationIndex(inputCombinationRow);

            noCombination++;

            //If the combination was not found
            if (combinationIndex == -1)
            {
                MessageBox.Show("The original combination was not found in the \"" + tableDataName + "\" table.",
                                "Combination not found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                chartCombination.Visible = false;
            }
            else
            {
                DataRow outputResultRow = dataRepository.GetDtData().Rows[combinationIndex - 1];

                double input;
                double output;

                chartCombination.Visible = true;
                chartCombination.Series["Result"].Points.Clear();

                for (int i = 0; i < outputColumnsName.Count; i++)
                {
                    //In case the value is null
                    if (outputResultRow.IsNull(i)) input = 0;
                    else input = (double)outputResultRow[inputColumnsName[i]];

                    //In case the value is null
                    if (outputResultRow.IsNull(i + inputColumnsName.Count)) output = 0;
                    else output = (double)outputResultRow[outputColumnsName[i]];

                    string label = "(" + inputColumnsName[i] + "=" + input.ToString() + " ; " + outputColumnsName[i] + "=" + output.ToString() + ")";

                    chartCombination.Series["Result"].Points.AddXY(input, output);
                    chartCombination.Series["Result"].Points[i].Label = label;
                }
            }
        }

        //When the "Load MS Access database" button is clicked
        private void buttonLoadDatabase_Click(object sender, EventArgs e)
        {
            MSAccessConnection msAccessConnection = new MSAccessConnection();

            string databasePath = msAccessConnection.FindDatabasePath();

            if (databasePath != null)
            {
                chartCombination.Visible = false;
                textFindCombination.Text = String.Empty;

                textLoadDatabase.Text = databasePath;

                //If the connection with the database succeed
                if (msAccessConnection.TryConnection())
                {
                    tableCombinationName = textCombinationName.Text;
                    tableDataName = textDataName.Text;
                    InputPrefix = textInputPrefix.Text;
                    OutputPrefix = textOutputPrefix.Text;

                    connection = msAccessConnection.GetConnection();

                    dbValidation = new DatabaseValidation(connection,
                                                          tableCombinationName,
                                                          tableDataName,
                                                          InputPrefix,
                                                          OutputPrefix);

                    //If the database succeed for all the validation checks
                    if (dbValidation.Validate()) noCombination = 0;
                    else textLoadDatabase.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("The connection to the MS Access database has failed.",
                                    "Unable to connect",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    textLoadDatabase.Text = String.Empty;
                }
            }
        }

        //Exit from the application
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Generate the combination string to display it on the form
        private string GetStrCombination(List<string> inputColumnsName,
                                         DataRow inputCombinationRow)
        {
            string strCombination = String.Empty;

            for (int i = 0; i < inputColumnsName.Count; i++)
            {
                strCombination += inputColumnsName[i] + "=" + inputCombinationRow[i] + "   ";
            }

            return strCombination;
        }

        //Return the name of something
        private string GetNameOf<T>(Expression<Func<T>> property)
        {
            return (property.Body as MemberExpression).Member.Name;
        }

        //Return the path of the "ValuesFormCombinationFinder.txt" file
        private string GetPathDefaultValues()
        {
            var fileName = GetNameOf(() => Properties.Resources.ValuesFormCombinationFinder);

            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            var filePath = path + @"Resources\" + fileName + ".txt";

            return filePath;
        }

        //When the form is loaded
        private void FormCombinationFinder_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon_Flogen_Technologies_64x64;

            displayDefaultValues();

            textLoadDatabase_TextChanged(null, null);
        }

        //Set to the form the default values from the "ValuesFormCombinationFinder.txt" file
        private void displayDefaultValues()
        {
            TextFileTool embeddedTextFileTool = new TextFileTool(valuesFilePath);

            List<string> linesFile = embeddedTextFileTool.ReadBatch();

            //Check if the file is empty
            if (!(linesFile.Count <= 1 && linesFile[0].Length == 0))
            {
                textCombinationName.Text = linesFile.Count >= 0 ? linesFile[0] : String.Empty;
                textDataName.Text = linesFile.Count >= 1 ? linesFile[1] : String.Empty;
                textInputPrefix.Text = linesFile.Count >= 2 ? linesFile[2] : String.Empty;
                textOutputPrefix.Text = linesFile.Count >= 3 ? linesFile[3] : String.Empty;
            }
        }

        //Write to the "ValuesFormCombinationFinder.txt" file the values in the form
        private void writeDefaultValues()
        {
            List<string> linesFile = new List<string>();

            if (textCombinationName.Text != String.Empty) linesFile.Add(textCombinationName.Text);
            if (textDataName.Text != String.Empty) linesFile.Add(textDataName.Text);
            if (textInputPrefix.Text != String.Empty) linesFile.Add(textInputPrefix.Text);
            if (textOutputPrefix.Text != String.Empty) linesFile.Add(textOutputPrefix.Text);

            TextFileTool embeddedTextFileTool = new TextFileTool(valuesFilePath);

            embeddedTextFileTool.WriteBatch(linesFile);
        }

        //Save the values of the form in a text file
        private void buttonSetDefault_Click(object sender, EventArgs e)
        {
            writeDefaultValues();

            MessageBox.Show("These values has been saved.");
        }

        //Enable or disable the "Find next combination" button
        private void textLoadDatabase_TextChanged(object sender, EventArgs e)
        {
            buttonFindCombination.Enabled = textLoadDatabase.Text == String.Empty ? false : true;
        }

        //Do a minimal formating of the text in each TextBox control
        private void TrimTextBox(TextBox textBox, int length)
        {
            string value = textBox.Text.Trim();

            textBox.Text = value.Length > length ? value.Substring(0, length) : value;
        }

        //Validate the value for each TextBox control
        private void ValidateTextBox(TextBox textBox, int length)
        {
            TrimTextBox(textBox, length);

            if (textBox.Text == String.Empty)
            {
                MessageBox.Show("This field can not be empty.",
                                "Validation error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                textBox.Undo();
                textBox.Focus();
            }
        }

        //Check if the combination table have not the same name as for the data table
        private void ValidateTableNotEqual(TextBox textBox)
        {
            if (textCombinationName.Text == textDataName.Text)
            {
                if (textCombinationName.Text == textDataName.Text)
                {
                    MessageBox.Show("The combination table name must not be the same as for the data table name.",
                                    "Validation error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    textBox.Undo();
                    textBox.Focus();
                }
            }
        }

        //Check if the output prefix is not the same as for the input prefix
        private void ValidatePrefixNotEqual(TextBox textBox)
        {
            if (textInputPrefix.Text == textOutputPrefix.Text)
            {
                MessageBox.Show("The output prefix must not be the same as for the input prefix.",
                                "Validation error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                textBox.Undo();
                textBox.Focus();
            }
        }

        //Validation before the TextBox update
        private void textCombinationName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textCombinationName, 15);
            ValidateTableNotEqual(textCombinationName);
        }

        //Validation before the TextBox update
        private void textDataName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textDataName, 15);
            ValidateTableNotEqual(textDataName);
        }

        //Validation before the TextBox update
        private void textInputPrefix_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textInputPrefix, 5);
            ValidatePrefixNotEqual(textInputPrefix);
        }

        //Validation before the TextBox update
        private void textOutputPrefix_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textOutputPrefix, 5);
            ValidatePrefixNotEqual(textOutputPrefix);
        }
    }
}
