using System;

namespace Proxy
{
    public class Subject_Car : ISubject_Car
    {
        public void DriveCar()
        {
            Console.WriteLine("Car has been driven!");
        }
    }
}
