﻿namespace Generic_Box_of_String
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

            int[] cellsIndexChanger = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int firstIndex = cellsIndexChanger[0];
            int secondIndex = cellsIndexChanger[1];
            box.SwapTheCells(firstIndex, secondIndex);
            
            Console.WriteLine(box.ToString());
            
        }
    }
}

