using System;

namespace Iterator
{
    //Un itérateur est un objet qui permet de parcourir tous les éléments contenus dans un autre objet,
    //le plus souvent un conteneur(liste, arbre, etc)

    //Utilisé lorsqu'un objet possède une collection de valeurs et qu'on désire y donner accès,
    //sans révéler la manière dont cette collection est représentée en interne

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ManageElements manageElements = new ManageElements();

            manageElements.AddElement("Element 1");
            manageElements.AddElement("Element 2");
            manageElements.AddElement("Element 3");
            manageElements.AddElement("Element 4");
            manageElements.AddElement("Element 5");
            manageElements.AddElement("Element 6");
            manageElements.AddElement("Element 7");
            //NOTE: Arrête à 5 car j'ai mis une condition dans "ManageElements.AddElement()"

            MainClass myMainClass = new MainClass(manageElements);

            //Affiche tous les éléments en console
            myMainClass.printElement();
        }
    }
}
