using System;

namespace Proxy
{
    //Un proxy est une classe se substituant à une autre classe

    //It is a class functioning as an interface to something else.
    //The proxy could interface to anything: a network connection, a large object in memory, a file,
    //or some other resource that is expensive or impossible to duplicate.
    //A proxy is a wrapper or agent object that is being called by the client
    //to access the real serving object behind the scenes.

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ISubject_Car car = new Proxy_Car(new Driver(16));
            car.DriveCar();

            car = new Proxy_Car(new Driver(25));
            car.DriveCar();
        }
    }
}
