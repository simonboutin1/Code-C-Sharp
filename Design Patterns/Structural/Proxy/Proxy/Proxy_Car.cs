using System;

namespace Proxy
{
    public class Proxy_Car : ISubject_Car
    {
        private Driver driver;
        private ISubject_Car realCar;

        public Proxy_Car(Driver driver)
        {
            this.driver = driver;
            realCar = new Subject_Car();
        }

        public void DriveCar()
        {
            if (driver.Age <= 16)
                Console.WriteLine("Sorry, the driver is too young to drive.");
            else
                realCar.DriveCar();
        }
}
}
