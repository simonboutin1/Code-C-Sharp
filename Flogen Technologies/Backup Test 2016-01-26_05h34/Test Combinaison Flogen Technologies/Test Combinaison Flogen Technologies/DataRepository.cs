using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Test_Combinaison_Flogen_Technologies
{
    class DataRepository
    {
        OleDbConnection connection;
        string tableCombinationName;
        string tableDataName;
        List<string> inputColumnsName;
        List<string> outputColumnsName;

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
            this.inputColumnsName = inputColumnsName;
            this.outputColumnsName = outputColumnsName;
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