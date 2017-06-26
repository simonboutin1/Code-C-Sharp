using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public class MainClass
    {
        ManageElements manageElements;

        public MainClass(ManageElements manageElements)
        {
            this.manageElements = manageElements;
        }

        //Affiche tous les éléments
        public void printElement()
        {
            IIterator myElementIterator = manageElements.createIterator();

            printElement(myElementIterator);
        }

        public void printElement(IIterator iterator)
        {
            while (iterator.HasNext())
            {
                Element element = (Element)iterator.Next();

                System.Console.WriteLine(element.Name);
            }
        }
    }
}
