class Hello
{
    // main method
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, world!");

        // TODO: Adds comment to a task list in Visual Studio

        /// Single-line comment used for documentation

        /** Multi-line comment 
            used for documentation **/



        int intNum = 9;
        long longNum = 9999999;
        float floatNum = 9.99F;
        double doubleNum = 99.999;
        decimal decimalNum = 99.9999M;
        char letter = 'D';
        bool @bool = true;
        string site = "quickref.me";

        var num = 999;
        var str = "999";
        var bo = false;



        Console.WriteLine("Enter number:");
        if (int.TryParse(Console.ReadLine(), out int input))
        {
            Console.WriteLine($"You entered {input}");
        }


        int j = 10;

        if (j == 10)
        {
            Console.WriteLine("I get printed");
        }
        else if (j > 10)
        {
            Console.WriteLine("I don't");
        }
        else
        {
            Console.WriteLine("I also don't");
        }

        char[] chars = new char[10];
        chars[0] = 'a';
        chars[1] = 'b';

        string[] letters = { "A", "B", "C" };
        int[] mylist = { 100, 200 };
        bool[] answers = { true, false };

        int[] numbers = { 1, 2, 3, 4, 5 };

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine(numbers[i]);
        }

        foreach (int num9 in numbers)
        {
            Console.WriteLine(num9);
        }


        string first = "John";
        string last = "Doe";

        string name = $"{first} {last}";
        Console.WriteLine(name); // => John Doe

        string longString = @"I can type any characters in here !#@$%^&*()__+ '' \n \t except double quotes and I will be taken literally. I even work with multiple lines."; //verbatim string

        Console.WriteLine("Enter number:");
        if (int.TryParse(Console.ReadLine(), out int input9))
        {
            // Input validated
            Console.WriteLine($"You entered {input9}");
        }




    }
}
