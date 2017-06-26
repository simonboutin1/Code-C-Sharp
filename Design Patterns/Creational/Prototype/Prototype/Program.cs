using System;

namespace Prototype
{
    //Prototype pattern refers to creating duplicate object while keeping performance in mind.
    //This type of design pattern comes under creational pattern as this pattern provides one
    //of the best ways to create an object.

    //Prototype est utilisé lorsque la création d'une instance est complexe
    //ou consommatrice en temps. Plutôt que créer plusieurs instances de la classe,
    //on copie la première instance et on modifie la copie de façon appropriée.

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create two instances and clone each

            ConcretePrototype1 p1 = new ConcretePrototype1("I");
            ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
            Console.WriteLine("Cloned: {0}", c1.Id);

            ConcretePrototype2 p2 = new ConcretePrototype2("II");
            ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
            Console.WriteLine("Cloned: {0}", c2.Id);
        }
    }
}
