namespace _01.GenericBoxofString
{
    using System.Text;
   public class Box<T>
    {
        private StringBuilder stringBuilder = new StringBuilder(); 

        public void PrintLines(T line)
        {
            stringBuilder.AppendLine($"{line.GetType()}: {line}");
        }

        public override string ToString() 
        {
            return stringBuilder.ToString().TrimEnd();
        }
    }
}
