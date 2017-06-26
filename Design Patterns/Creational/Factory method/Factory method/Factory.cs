using System;

namespace Factory_method
{
    public enum PeopleType
    {
        RURAL,
        URBAN
    }

    /// <summary>
    /// Implementation of Factory - Used to create objects
    /// </summary>
    public class Factory
    {
        public IPeople GetPeople(PeopleType type)
        {
            switch (type)
            {
                case PeopleType.RURAL:
                    return new PeopleVillage();
                case PeopleType.URBAN:
                    return new PeopleCity();
                default:
                    throw new Exception();
             }
        }
    }
}
