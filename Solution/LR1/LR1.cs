namespace ConsoleApp1
{
    internal static class Lab
    {
        private static void Main()
        {
            Console.WriteLine("Choose the solution method: 1 - Without recursion, 2 - With recursion");
            var x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    Console.WriteLine("Solution without recursion");
                    Console.WriteLine("Please, enter the member number of the Fibonacci series");
                    var y = Convert.ToInt32(Console.ReadLine());
                    var answer = ForSolution(y);
                    Console.WriteLine($"Your Fibonacci number is {answer}");
                    break;
                case 2:
                    Console.WriteLine("Solution with recursion");
                    Console.WriteLine("Please, enter the member number of the Fibonacci series");
                    var z = Convert.ToInt32(Console.ReadLine());
                    var answer1 = RecursionSolution(z);
                    Console.WriteLine($"Your Fibonacci number is {answer1}");
                    break;
            }

        }

        private static int ForSolution(int count)
        {
            var number1 = 0;
            var number2 = 1;
            for (var i = 0; i < count; i++)
            {
                var c = number2;
                number2 = number1 + c;
                number1 = c;
            }

            return number1;
        }

        private static int RecursionSolution(int count)
        {
            if (count is 1 or 2)
            {
                return 1;
            }
            return RecursionSolution(count - 1) + RecursionSolution(count - 2);
        }
    }
}

