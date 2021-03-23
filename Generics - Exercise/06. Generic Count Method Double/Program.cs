namespace Generic_Box_of_String
{
    using System;
    class Program
    {
        public static void Main()
        {
            int countOfLines = int.Parse(Console.ReadLine());
            Box<double> box = new Box<double>();

            for (int i = 0; i < countOfLines; i++)
            {
                var inputLine = Console.ReadLine();
                box.AddToList(double.Parse(inputLine));
            }
            Double compare = double.Parse(Console.ReadLine());
            int result = box.CountIfBigger(compare);
            Console.WriteLine(result);
        }
    }
}

