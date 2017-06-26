using System;

namespace Memento
{
    public class Memento
    {
        private String state;

        public Memento(String state)
        {
            this.state = state;
        }

        public String State
        {
            get { return state; }
            // set { state = value; }
        }
    }
}
