using System.Collections.Generic;

namespace Observer
{
    public class Subject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private int decimalNumber;

        public Subject(int decimalNumber)
        {
            this.decimalNumber = decimalNumber;
        }

        public int DecimalNumber
        {
            get
            {
                return this.decimalNumber;
            }

            set
            {
                this.decimalNumber = value;

                Notify();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            observers.ForEach(x => x.Update());
        }
    }
}
