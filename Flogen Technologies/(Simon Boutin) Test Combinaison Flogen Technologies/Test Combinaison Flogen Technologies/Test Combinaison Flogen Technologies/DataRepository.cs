using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

//This class store all the data from the combination and data tables
namespace Test_Combinaison_Flogen_Technologies
{
    class DataRepository
    {
        string tableCombinationName;
        string tableDataName;

        OleDbConnection connection;
        DataTable dtCombination;
        DataTable dtData;

        public DataRepository(OleDbConnection connection, 
                              string tableCombinationName,
                              string tableDataName,
                              List<string> inputColumnsName,
                              List<string> outputColumnsName)
        {
            this.connection = connection;
            this.tableCombinationName = tableCombinationName;
            this.tableDataName = tableDataName;
        }

        //Fill the dtCombination DataTable
        public void FillDtCombination()
        {
            string sql = "SELECT * FROM " + tableCombinationName;

            OleDbCommand cmd = new OleDbCommand(sql, connection);

            dtCombination = new DataTable();
            dtCombination.Load(cmd.ExecuteReader());
        }

        //Fill the dtData DataTable
        public void FillDtData()
        {
            string sql = "SELECT * FROM " + tableDataName;

            OleDbCommand cmd = new OleDbCommand(sql, connection);

            dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
        }

        //Return the dtCombination DataTable
        public DataTable GetDtCombination()
        {
            return dtCombination;
        }

        //Return the dtData DataTable
        public DataTable GetDtData()
        {
            return dtData;
        }
    }
}