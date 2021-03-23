namespace _07.TupleDemo
{
    using System;
    class Program
    {
        static void Main()
        {
            var firstLineInput = Console.ReadLine().Split();
            var fullName = firstLineInput[0] + " " + firstLineInput[1];
            var address = firstLineInput[2];
            Tuple<string, string> stringTuple = new Tuple<string, string>(fullName, address);
            Console.WriteLine(stringTuple);

            var secondLineInput = Console.ReadLine().Split();
            var name = secondLineInput[0];
            var liters = int.Parse(secondLineInput[1]);
            Tuple<string, int> nameAndLiters = new Tuple<string, int>(name, liters);
            Console.WriteLine(nameAndLiters.ToString());

            var thirdLineInput = Console.ReadLine().Split();
            var integer = int.Parse(thirdLineInput[0]);
            var floatingPoint = double.Parse(thirdLineInput[1]);
            Tuple<int, double> nameAndLiter = new Tuple<int, double>(integer, floatingPoint);
            Console.WriteLine(nameAndLiter.ToString());
        }
    }
}
