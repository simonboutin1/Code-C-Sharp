using System;

namespace Strategy
{
    //Strategy enables an algorithm's behavior to be selected at runtime.

    //Strategy est utile pour des situations où il est nécessaire de permuter dynamiquement les algorithmes
    //utilisés dans une application. Le patron stratégie est prévu pour fournir le moyen de définir
    //une famille d'algorithmes, encapsuler chacun d'eux en tant qu'objet, et les rendre interchangeables.
    //Ce patron laisse les algorithmes changer indépendamment des clients qui les emploient.

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CalculateClient minusClient = new CalculateClient(new Strategy_Minus());
            Console.WriteLine("Minus: " + minusClient.Calculate(7, 1).ToString());

            CalculateClient plusClient = new CalculateClient(new Strategy_Plus());
            Console.WriteLine("Plus: " + plusClient.Calculate(7, 1).ToString());
        }
    }
}
