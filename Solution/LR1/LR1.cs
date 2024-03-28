namespace ConsoleApp1
{
    internal static class Lab
    {
        private static void Main()
        {
            Console.WriteLine("1. - Palindromes, 2. - Array");
            switch (Convert.ToInt16(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine("Please, enter your line");
                    var x = Convert.ToString(Console.ReadLine());
                    if (x != null)
                    {
                        Console.WriteLine(IsItPalindrome(x) ? "Your line is a palindrome" : "Your line is not a palindrome");
                    }
                    else
                    {
                        Main();
                    }
                    break;
                case 2:
                    var array = InputArray();
                    Console.WriteLine("Please, enter the number for duplication: ");
                    var number = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine();
                    array = DuplicateNumber(array, number);
                    OutputArray(array);
                    break;
            }
        }

        private static void OutputArray(int[] array)
        {
            foreach (var element in array)
            {
                Console.WriteLine($"{element} ");
            }
        }

        private static int[] DuplicateNumber(int[] array, int number)
        {
            var size = array.Length;
            var newArray = new int[size];
            var count = 0;
            for (var i = 0; i < size - 1; i++)
            {
                if (i + count >= size) continue;
                newArray[i+count] = array[i];
                if (array[i] != number) continue;
                count += 1;
                if (i + count >= size) continue;
                newArray[i + count] = number;
            }
            return newArray;
        }

        private static int[] InputArray()
        {
            Console.WriteLine("Please, enter the size of your array: ");
            var size = Convert.ToInt16(Console.ReadLine());
            var array = new int[size]; 
            Console.WriteLine("Please, enter your array: ");
            for (int i = 0; i < size; i++)
            {
                array[i] = Convert.ToInt16(Console.ReadLine());
            }
            return array;
        }

        private static bool IsItPalindrome(string line)
        {
            var flag = true;
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] != line[line.Length - i - 1])
                {
                    flag = false;
                }
            }
            return flag;
        }
    }
}

