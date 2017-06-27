using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.Linq.Expressions;
using System.IO;

namespace Test_Combinaison_Flogen_Technologies
{
    public partial class FormCombinationFinder : Form
    {
        //NOTE: The directory "\Resources\..." must be copied in the same directory as the .exe

        //NOTE: On the properties window of the ressource text file, set the "Build Action" property to "None" since I must edit it.
        //      Set also the "Copy To Output Directory" to "Copy if newer"
        //NOTE: If I only need to read it, set the "Build Action" property to "Embedded Resource"
        private string valuesFilePath;

        public FormCombinationFinder()
        {
            InitializeComponent();

            valuesFilePath = GetPathDefaultValues();
        }

        private void buttonFindCombination_Click(object sender, EventArgs e)
        {
            Font myfont = new Font("Arial", 12, System.Drawing.FontStyle.Regular);

            chartCombination.Series["test1"].Points.Clear();
            chartCombination.Series["test2"].Points.Clear();


            Random rdn = new Random();

            for (int i = 0; i < 3; i++)
            {
                chartCombination.Series["test1"].Points.AddXY
                                (rdn.Next(0, 10), rdn.Next(0, 10));
                chartCombination.Series["test2"].Points.AddXY
                                (rdn.Next(0, 10), rdn.Next(0, 10));
            }

            //            chart1.Series["test1"].Points[5].Label = "milieu";
            //            chart1.Series["test1"].SmartLabelStyle.Enabled = true;

            //  chart1.Series["test1"].Points[5].
            // chart1.Series["test1"].SmartLabelStyle.Enabled = true;

            chartCombination.Series["test1"].ChartType = SeriesChartType.Line;
            chartCombination.Series["test1"].Color = Color.Red;

            //Display the label associate with a specific point
            //NOTE: Don't use "SeriesChartType.FastLine" if you want to display the point label

            //  chart1.Series["test1"].IsValueShownAsLabel = true;

            //            chart1.Series["test1"].Points[1].IsValueShownAsLabel = true;

            chartCombination.Series["test1"].Font = myfont;
            chartCombination.Series["test1"].Points[1].Label = "(2, 4)";
            chartCombination.Series["test1"].Points[1].MarkerStyle = MarkerStyle.Cross;
            chartCombination.Series["test1"].Points[1].MarkerSize = 12;


            chartCombination.Series["test2"].ChartType = SeriesChartType.Line;
            chartCombination.Series["test2"].Color = Color.Blue;

            chartCombination.ChartAreas[0].AxisX.TitleFont = myfont;
            chartCombination.ChartAreas[0].AxisX.Title = "Combination";

            chartCombination.ChartAreas[0].AxisY.TitleFont = myfont;
            chartCombination.ChartAreas[0].AxisY.Title = "Result";
        }

        private void buttonLoadDatabase_Click(object sender, EventArgs e)
        {
            MSAccessConnection msAccessConnection = new MSAccessConnection();

            string databasePath = msAccessConnection.FindDatabasePath();

            if (databasePath != null)
            {
                textLoadDatabase.Text = databasePath;

                //If the connection with the database succeed
                if (msAccessConnection.TryConnection())
                {
                    string tableCombinationName = textCombinationName.Text;
                    string tableDataName = textDataName.Text;
                    string InputPrefix = textInputPrefix.Text;
                    string OutputPrefix = textOutputPrefix.Text;

                    OleDbConnection connection = msAccessConnection.GetConnection();

                    DatabaseValidation dbValidation = new DatabaseValidation(connection,
                                                                             tableCombinationName,
                                                                             tableDataName,
                                                                             InputPrefix,
                                                                             OutputPrefix);

                    //If the database succeed for all the validation checks
                    if (dbValidation.Validate())
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

                        DataRow row = dataRepository.GetDtCombination().Rows[0];

                        int combinationIndex = searchTool.GetCombinationIndex(row);

                        //If the combination was not found
                        if (combinationIndex == -1)
                        {
                            MessageBox.Show("The original combination was not found in the \"" + tableDataName + "\" table.",
                                            "Combination not found",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);

                            textLoadDatabase.Text = String.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Combination index = " + combinationIndex.ToString());
                        }
                    }
                    else
                    {
                        textLoadDatabase.Text = String.Empty;
                    }
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

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        static string GetNameOf<T>(Expression<Func<T>> property)
        {
            return (property.Body as MemberExpression).Member.Name;
        }

        private string GetPathDefaultValues()
        {
            var fileName = GetNameOf(() => Properties.Resources.ValuesFormCombinationFinder);

            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            var filePath = path + @"Resources\" + fileName + ".txt";

            return filePath;
        }

        private void FormCombinationFinder_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon_Flogen_Technologies_64x64;

            displayDefaultValues();

            textLoadDatabase_TextChanged(null, null);
        }

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

        private void buttonSetDefault_Click(object sender, EventArgs e)
        {
            writeDefaultValues();

            MessageBox.Show("These values has been saved.");
        }

        private void textLoadDatabase_TextChanged(object sender, EventArgs e)
        {
            buttonFindCombination.Enabled = textLoadDatabase.Text == String.Empty ? false : true;
        }

        private void TrimTextBox(TextBox textBox, int length)
        {
            string value = textBox.Text.Trim();

            textBox.Text = value.Length > length ? value.Substring(0, length) : value;
        }

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

        private void textCombinationName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textCombinationName, 15);
            ValidateTableNotEqual(textCombinationName);
        }

        private void textDataName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textDataName, 15);
            ValidateTableNotEqual(textDataName);
        }

        private void textInputPrefix_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textInputPrefix, 5);
            ValidatePrefixNotEqual(textInputPrefix);
        }

        private void textOutputPrefix_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(textOutputPrefix, 5);
            ValidatePrefixNotEqual(textOutputPrefix);
        }
    }
}
