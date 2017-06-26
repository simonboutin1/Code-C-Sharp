using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public class ElementIterator : IIterator
    {
        private ArrayList elements;
        private int position = 0;

        public ElementIterator(ArrayList elements)
        {
            this.elements = elements;
        }

        //Vérifie s'il y a un élément suivant dans la liste
        public bool HasNext()
        {
            return (position >= elements.Count) ? false : true;
        }

        //Retourne l'élément courrant et se positionne sur l'élément suivant
        public Object Next()
        {
            Element currentElement = (Element)elements[position];
            position++;

            return currentElement;
        }
    }
}
