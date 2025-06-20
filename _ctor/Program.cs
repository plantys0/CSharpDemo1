namespace _ctor
{
    public class IntegerHolder
    {
        public int Value { get; set; }

        public IntegerHolder(int value)
        {
            Value = value;
        }
    }
    public class IntegerHolderNoCtor
    {
        public int Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // With constructor
            int input = 5;
            IntegerHolder integerHolder = new IntegerHolder(input);
            int output = integerHolder.Value + 2;
            Console.WriteLine($"Output with constructor: {output}");

            // Without constructor
            IntegerHolderNoCtor integerHolderNoCtor = new IntegerHolderNoCtor();
            integerHolderNoCtor.Value = input;
            int outputNoCtor = integerHolderNoCtor.Value + 2;
            Console.WriteLine($"Output without constructor: {outputNoCtor}");

            // @@try other variants
        }
    }
}