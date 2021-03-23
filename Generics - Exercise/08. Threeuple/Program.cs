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
            var town = firstLineInput[3];
            var stringTuple = new Threeuple<string, string, string>(fullName, address, town);
            Console.WriteLine(stringTuple);

            var secondLineInput = Console.ReadLine().Split();
            var name = secondLineInput[0];
            var liters = int.Parse(secondLineInput[1]);
            var ifDrunk = secondLineInput[2] == "drunk" ? true : false;
            var nameAndLiters = new Threeuple<string, int, bool>(name, liters, ifDrunk);
            Console.WriteLine(nameAndLiters);

            var thirdLineInput = Console.ReadLine().Split();
            name = thirdLineInput[0];
            var floatingPoint = double.Parse(thirdLineInput[1]);
            var bankName = thirdLineInput[2];
            var nameAndBankAccount = new Threeuple<string, double, string>(name, floatingPoint, bankName);
            Console.WriteLine(nameAndBankAccount);
        }
    }
}
