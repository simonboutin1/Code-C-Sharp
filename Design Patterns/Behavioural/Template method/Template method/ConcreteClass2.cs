using System;

namespace Template_method
{
    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    class ConcreteClass2 : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("ConcreteClass2.PrimitiveOperation1()");
        }

        public override void PrimitiveOperation2()
        {
            Console.WriteLine("ConcreteClass2.PrimitiveOperation2()");
        }
     }
}
