using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Test_Combinaison_Flogen_Technologies
{
    public class MSAccessConnection
    {
        OleDbConnection connection;
        string databasePath;

        //Display a dialog to find a MS Access database and return its path
        public string FindDatabasePath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

          //  openFileDialog1.InitialDirectory = @"c:\";
            openFileDialog1.Filter = "accdb files (*.accdb)|*.accdb";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false; //Keep in memory the previous selected directory

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog

            databasePath = (result == DialogResult.OK) ? openFileDialog1.FileName : null;

            return databasePath;
        }

        //Try to make a connection with the database
        public bool TryConnection()
        {
            bool connectionSucceed = false;

            if (databasePath != null)
            {
                connection = new OleDbConnection();
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source= " + databasePath;

                try
                {
                    connection.Open();

                    connectionSucceed = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return connectionSucceed;
        }

        //Close the connection to the database
        public void CloseConnection()
        {
            connection.Close();
        }

        //Return the connection object
        public OleDbConnection GetConnection()
        {
            return connection;
        }
    }
}
