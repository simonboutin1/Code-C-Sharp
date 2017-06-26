using System;

namespace Observer
{
    public class ObserverBinary : IObserver
    {
        Subject subject;

        public ObserverBinary(Subject subject)
        {
            this.subject = subject;
        }

        public void Update()
        {
            Console.WriteLine("ObserverBinary: {0} give {1} in binary base", subject.DecimalNumber, Convert.ToString(subject.DecimalNumber, 2));
        }
    }
}
