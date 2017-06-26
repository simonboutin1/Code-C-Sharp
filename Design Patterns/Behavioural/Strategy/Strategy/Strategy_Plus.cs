namespace Strategy
{
    class Strategy_Plus : IStrategy
    {
        public int Calculate(int value1, int value2)
        {
            return value1 + value2;
        }
    }
}
