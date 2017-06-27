using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

//This class contain method to read and write in a text file
namespace GeneralTools
{
    public class TextFileTool
    {
        string filePath;
        List<string> readLines;

        public TextFileTool(string filePath)
        {
            this.filePath = filePath;
        }

        //Read a batch of lines from the text file
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

        //Write a batch of lines into the text file
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
