namespace Strategy
{
    class Strategy_Minus : IStrategy
    {
        public int Calculate(int value1, int value2)
        {
            return value1 - value2;
        }
    }
}