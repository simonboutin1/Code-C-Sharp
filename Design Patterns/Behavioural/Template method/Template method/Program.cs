using System;

namespace Template_method
{
    //Template method définit le squelette d'un algorithme à l'aide d'opérations abstraites
    //dont le comportement concret se trouvera dans les sous-classes, qui implémenteront ces opérations.

    //Template method define the skeleton of an algorithm in an operation, deferring some steps to subclasses.
    //Template Method lets subclasses redefine certain steps of an algorithm without changing
    //the algorithm's structure.

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AbstractClass aA = new ConcreteClass1();
            aA.TemplateMethod();

            AbstractClass aB = new ConcreteClass2();
            aB.TemplateMethod();
        }
    }
}
