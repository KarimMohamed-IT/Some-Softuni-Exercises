namespace _01.GenericBoxofString
{
    using System;
    public class Program
    {
       public static void Main()
        {
            int countOfLines = int.Parse(Console.ReadLine());
            var box = new Box<string>();
            for (int i = 0; i < countOfLines; i++)
            {
                var line = Console.ReadLine();
                box.PrintLines(line);
            }
                Console.WriteLine(box); 
        }
    }
}
