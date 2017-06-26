using System;

namespace Observer
{
    public class ObserverOctal : IObserver
    {
        Subject subject;

        public ObserverOctal(Subject subject)
        {
            this.subject = subject;
        }

        public void Update()
        {
            Console.WriteLine("ObserverOctal: {0} give {1} in octal base", subject.DecimalNumber, Convert.ToString(subject.DecimalNumber, 8));
        }
    }
}
