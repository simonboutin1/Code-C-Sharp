using System;

namespace Factory_method
{
    //Factory method permet d'instancier des objets dont le type est dérivé d'un type abstrait.
    //La classe exacte de l'objet n'est donc pas connue par l'appelant

    //Use factory method to deal with the problem of creating objects
    //without having to specify the exact class of the object that will be created

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Factory factory = new Factory();

            IPeople people1 = factory.GetPeople(PeopleType.RURAL);
            IPeople people2 = factory.GetPeople(PeopleType.URBAN);

            Console.WriteLine(people1.GetName());
            Console.WriteLine(people2.GetName());
        }
    }
}