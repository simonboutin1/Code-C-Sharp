namespace Strategy
{
    class CalculateClient
    {
        private IStrategy calculateStrategy;

        //Constructor: assigns strategy to interface
        public CalculateClient(IStrategy strategy)
        {
            this.calculateStrategy = strategy;
        }

        //Executes the strategy
        public int Calculate(int value1, int value2)
        {
            return calculateStrategy.Calculate(value1, value2);
        }
    }
}
