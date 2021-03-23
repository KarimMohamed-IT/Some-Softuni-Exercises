using System;
using System.Collections.Generic;
using System.Text;

namespace Generic_Box_of_String
{
    public class Box<T>
    {
        private List<T> list = new List<T>();
        T line;

        public void AddToList(T line)
        {
            this.line = line;
            list.Add(line);
        }

        public void SwapTheCells(int firstIndex , int secondIndex)
        {
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in list)
            {
               stringBuilder.AppendLine($"{item.GetType()}: {item.ToString()}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
    }
}
