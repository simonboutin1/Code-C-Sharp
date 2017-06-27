using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Test_Combinaison_Flogen_Technologies
{
    public class TextFileTool
    {
        string filePath;
        List<string> readLines;

        public TextFileTool(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string> ReadBatch()
        {
            readLines = new List<string>();
            int nbLines;

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(filePath);

                //Read the first line of text
                readLines.Add(sr.ReadLine());
                nbLines = readLines.Count;

                //Continue to read until you reach end of file
                while (readLines[nbLines - 1] != null)
                {
                    //Read the next line
                    readLines.Add(sr.ReadLine());
                    nbLines = readLines.Count;
                }

                //Remove the last line since it was null
                readLines.RemoveAt(nbLines - 1);
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }

            return readLines;
        }

        public void WriteBatch(List<string> writeLines)
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(filePath);

                for (int i = 0; i < writeLines.Count; i++)
                {
                    //Write a line of text
                    sw.WriteLine(writeLines[i]);
                }

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
        }
    }
}
