using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

//This class perform all the necessary validation at the opening of the MS Access database
namespace Test_Combinaison_Flogen_Technologies
{
    class DatabaseValidation
    {
        const string ENTER = "\r\n";

        OleDbConnection connection;

        string combinationTableName;
        string dataTableName;
        string inputPrefix;
        string outputPrefix;

        List<string> inputColumnsName;
        List<string> outputColumnsName;

        public DatabaseValidation(OleDbConnection connection,
                                  string combinationTableName,
                                  string dataTableName,
                                  string inputPrefix,
                                  string outputPrefix)
        {
            this.connection = connection;

            this.combinationTableName = combinationTableName;
            this.dataTableName = dataTableName;
            this.inputPrefix = inputPrefix;
            this.outputPrefix = outputPrefix;

            inputColumnsName = new List<string>();
            outputColumnsName = new List<string>();
        }

        //Perform all the database validation
        public bool Validate()
        {
            //Validate the combination table
            if (TableNameExists(combinationTableName))
            {
                if (CorrectInputPattern())
                {
                    if (!RecordExists(combinationTableName)) return false;
                }
                else return false;
            }
            else return false;

            //Validate the data table
            if (TableNameExists(dataTableName))
            {
                if (InputColumnsNameMatch())
                {
                    if (CorrectOutputFormat())
                    {
                        if (!RecordExists(dataTableName)) return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;

            return true;
        }

        //Check if a table name exists in the database connection
        private bool TableNameExists(string tableName)
        {
            bool tableExists = connection.GetSchema("Tables", new string[4] { null, null, tableName, "TABLE" }).Rows.Count > 0;

            if (!tableExists)
            {
                MessageBox.Show("The table \"" + tableName + "\" doesn't exists in the database.",
                                "Table name doesn't exists",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            return tableExists;
        }
 
        //Check if a table contain at least 1 record
        private bool RecordExists(string tableName)
        {
            string sql = "SELECT COUNT(*) FROM " + tableName;

            OleDbCommand command = new OleDbCommand(sql, connection);

            int result = (int)command.ExecuteScalar();

            bool recordExists = result > 0 ? true : false;

            if (!recordExists)
            {
                MessageBox.Show("The table \"" + tableName + "\" contain no record.",
                                "Record not found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            return recordExists;
        }

        //Check if the "Combination" table columns name have the correct pattern
        private bool CorrectInputPattern()
        {
            string sql = "SELECT * FROM " + combinationTableName;

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader dataReader;
            dataReader = command.ExecuteReader();   //Get the query results 

            string validName;
            bool correctFormat = true;

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                validName = inputPrefix + (i + 1).ToString();

                if (dataReader.GetName(i).Equals(validName, StringComparison.Ordinal) == false)
                {
                    MessageBox.Show("In the \"" + combinationTableName + "\" table, all consecutive column names must follow this pattern:" + 
                                    ENTER + 
                                    ENTER +
                                    inputPrefix + "1  " + inputPrefix + "2  " + inputPrefix + "3  " + inputPrefix + "4...",
                                    "Invalid column names",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    correctFormat = false;
                    break;
                }
                else
                {
                    inputColumnsName.Add(dataReader.GetName(i));
                }
            }

            dataReader.Close();
            command.Dispose();

            return correctFormat;
        }

        //Check if the "Combination" table columns name match with the "Data" table input columns name
        private bool InputColumnsNameMatch()
        {
            string sql = "SELECT * FROM " + dataTableName;

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader dataReader;
            dataReader = command.ExecuteReader();   //Get the query results 

            bool columnNamesMatch = true;

            for (int i = 0; i < inputColumnsName.Count; i++)
            {
                if (dataReader.GetName(i).Equals(inputColumnsName[i], StringComparison.Ordinal) == false)
                {
                    MessageBox.Show("Input column names mismatch between the \"" + dataTableName + "\" and the \"" + combinationTableName + "\" tables.",
                                    "Input column names mismatch",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    columnNamesMatch = false;
                    break;
                }
            }

            dataReader.Close();
            command.Dispose();

            return columnNamesMatch;
        }

        //Check if the "Data" table output columns name have the correct pattern
        private bool CorrectOutputFormat()
        {
            string sql = "SELECT * FROM " + dataTableName;

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader dataReader;
            dataReader = command.ExecuteReader();   //Get the query results 

            string validName;
            bool correctFormat = true;
            int suffix = 0;

            for (int i = inputColumnsName.Count; i < dataReader.FieldCount; i++)
            {
                validName = outputPrefix + (++suffix).ToString();

                if (dataReader.GetName(i).Equals(validName, StringComparison.Ordinal) == false)
                {
                    MessageBox.Show("In the \"" + dataTableName + "\" table, all consecutive results column names must follow this pattern:" +
                                    ENTER +
                                    ENTER +
                                    outputPrefix + "1  " + outputPrefix + "2  " + outputPrefix + "3  " + outputPrefix + "4...",
                                    "Invalid results column names",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    correctFormat = false;
                    break;
                }
                else
                {
                    outputColumnsName.Add(dataReader.GetName(i));
                }
            }

            dataReader.Close();
            command.Dispose();

            return correctFormat;
        }

        //Return the input columns name list
        public List<string> GetInputColumnsName()
        {
            return inputColumnsName;
        }

        //Return the output columns name list
        public List<string> GetOutputColumnsName()
        {
            return outputColumnsName;
        }
    }
}
