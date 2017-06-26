using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public class ManageElements
    {
        const int MAX_ELEMENTS = 5;

        //ArrayList permet d'avoir un array qui grossi dynamiquement 
        ArrayList elements = new ArrayList();

        //Ajoute un élément
        public void AddElement(string name)
        {
            if (elements.Count < MAX_ELEMENTS)
            {
                elements.Add(new Element(name));
            }
        }

        //Crée un itérateur
        public IIterator createIterator()
        {
            return new ElementIterator(elements);
        }
    }
}
