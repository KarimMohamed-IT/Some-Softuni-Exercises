namespace Generic_Box_of_String
{
    using System;
    using System.Linq;
    class Program
    {
        public static void Main()
        {
            int countOfLines = int.Parse(Console.ReadLine());
            Box<string> box = new Box<string>();

            for (int i = 0; i < countOfLines; i++)
            {
                var inputLine = Console.ReadLine();
                box.AddToList(inputLine);
            }
            string compare = Console.ReadLine();
            int result = box.CountIfBigger(compare);
            Console.WriteLine(result);
        }
    }
}

