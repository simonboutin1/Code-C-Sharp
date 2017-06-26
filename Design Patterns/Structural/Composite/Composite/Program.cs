using System;

namespace Composite
{
    //Composite permet de concevoir une structure d'arbre
    //constitué d'un ou de plusieurs objets similaires (ayant des fonctionnalités similaires).
    //L'idée est de manipuler un groupe d'objets de la même façon que s'il s'agissait d'un seul objet.
    //Les objets ainsi regroupés doivent posséder des opérations communes, c'est-à-dire un "dénominateur commun".

    //The composite pattern describes that a group of objects is to be treated in the same way
    //as a single instance of an object. The intent of a composite is to "compose" objects
    //into tree structures to represent part-whole hierarchies.
    //Implementing the composite pattern lets clients treat individual objects and compositions uniformly.

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // initialize variables
            var compositeGraphic = new CompositeGraphic();
            var compositeGraphic1 = new CompositeGraphic();
            var compositeGraphic2 = new CompositeGraphic();

            //Add 1 Graphic to compositeGraphic1
            compositeGraphic1.Add(new Ellipse());

            //Add 2 Graphic to compositeGraphic2
            compositeGraphic2.AddRange(new Rectangle(),
                                       new Ellipse());

            /*Add 1 Graphic, compositeGraphic1, and 
              compositeGraphic2 to compositeGraphic */
            compositeGraphic.AddRange(new Rectangle(),
                                      compositeGraphic1,
                                      compositeGraphic2);

            /*Prints the complete graphic 
            (two Ellipse and two Rectangle").*/
            compositeGraphic.Print();
        }
    }
}
