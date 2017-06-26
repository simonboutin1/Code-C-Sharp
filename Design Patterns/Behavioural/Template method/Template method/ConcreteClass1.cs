using System;

namespace Template_method
{
    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    class ConcreteClass1 : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("ConcreteClass1.PrimitiveOperation1()");
        }

        public override void PrimitiveOperation2()
        {
            Console.WriteLine("ConcreteClass1.PrimitiveOperation2()");
        }
    }
}
