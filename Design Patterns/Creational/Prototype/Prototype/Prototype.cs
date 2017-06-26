namespace Prototype
{
    /// <summary>
    /// The 'Prototype' abstract class
    /// </summary>
    abstract class Prototype
    {
        private string id;

        // Constructor
        public Prototype(string id)
        {
            this.id = id;
        }

        // Gets id
        public string Id
        {
            get { return id; }
        }

        public abstract Prototype Clone();
    }
}
