using System;

namespace Memento
{
    public class Originator
    {
        private String state;

        public String State
        {
            get { return state; }
            set { state = value; }
        }

        public Memento saveStateToMemento()
        {
            return new Memento(state);
        }

        public void getStateFromMemento(Memento Memento)
        {
            state = Memento.State;
        }
    }
}
