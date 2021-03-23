namespace _02.GenericBoxofInteger
{
    using System;
   public class Program
    {
       public static void Main()
        {
            int countOfLines = int.Parse(Console.ReadLine());
            Box<int> box = new Box<int>();
            for (int i = 0; i < countOfLines; i++)
            {
                var line = int.Parse(Console.ReadLine());
                box.PrintLines(line);
            }
            Console.WriteLine(box);
        }
    }
}
