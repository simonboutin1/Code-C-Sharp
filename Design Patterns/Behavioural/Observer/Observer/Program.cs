using System;

namespace Observer
{
    //Observer est utilisé pour envoyer un signal à des modules qui jouent le rôle d'observateurs.
    //En cas de notification, les observateurs effectuent alors l'action adéquate en fonction
    //des informations qui parviennent depuis les modules qu'ils observent.

    //In Observer an object, called the subject, maintains a list of its dependents, called observers,
    //and notifies them automatically of any state changes, usually by calling one of their methods.
    //It is mainly used to implement distributed event handling systems.
    //It is also a key part in the familiar model–view–controller (MVC) architectural pattern.

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Subject subject = new Subject(5);
            
            //Subscribe the observers
            subject.Subscribe(new ObserverBinary(subject));
            subject.Subscribe(new ObserverOctal(subject));


            //Will notify all subjects
            subject.DecimalNumber++;

            //Will notify all subjects
            subject.DecimalNumber++;

            //Will notify all subjects
            subject.DecimalNumber++;
        }
    }
}
