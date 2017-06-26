using System;

namespace Memento
{
    //Mémento permet de restaurer un état précédent d'un objet (retour arrière)
    //sans violer le principe d'encapsulation

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Originator originator = new Originator();
            CareTaker careTaker = new CareTaker();

            originator.State = "State #1"; //Was not saved
            originator.State = "State #2";
            careTaker.Add(originator.saveStateToMemento()); //Save the state

            originator.State = "State #3";
            careTaker.Add(originator.saveStateToMemento()); //Save the state

            originator.State = "State #4";
            System.Console.WriteLine("Current State: " + originator.State);
            careTaker.Add(originator.saveStateToMemento()); //Save the state


            //Retreive the states
            originator.getStateFromMemento(careTaker.Get(0));
            System.Console.WriteLine("First saved State: " + originator.State);

            originator.getStateFromMemento(careTaker.Get(1));
            System.Console.WriteLine("Second saved State: " + originator.State);

            originator.getStateFromMemento(careTaker.Get(2));
            System.Console.WriteLine("Third saved State: " + originator.State);
        }
    }
}
