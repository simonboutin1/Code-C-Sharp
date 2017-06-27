using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace Test_Combinaison_Flogen_Technologies
{
    class SearchTool
    {
        DataTable dtData;
        List<string> inputColumnsName;
        DataRow inputCombination;
        int indexCombination = 1;

        public SearchTool(DataTable dtData,
                          List<string> inputColumnsName)
        {
            this.dtData = dtData;
            this.inputColumnsName = inputColumnsName;
        }

        //Return the combination index
        public int GetCombinationIndex(DataRow inputCombination)
        {
            this.inputCombination = inputCombination;

            return recursiveSearch(new DataView(dtData), 0);
        }

        //Recursive method that return the combination index
        private int recursiveSearch(DataView dvData, int noColumn)
        {
            if (noColumn < inputColumnsName.Count)
            {
                string columnValue = inputCombination[noColumn].ToString().Replace(",", ".");

                dvData.RowFilter = inputColumnsName[noColumn] + " < " + columnValue;
                indexCombination += dvData.Count;
                dvData.RowFilter = inputColumnsName[noColumn] + " = " + columnValue;

                if (dvData.Count > 0)
                {
                    DataView newDvData = new DataView(dvData.ToTable());

                    //Hope it will free the memory
                    dvData.Dispose();

                    recursiveSearch(newDvData, noColumn + 1);
                }
                else
                {
                    //If the combination was not found
                    indexCombination = -1;
                }
            }

            return indexCombination;
        }
    }
}
