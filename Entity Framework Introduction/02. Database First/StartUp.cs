using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            var context = new SoftUniContext();
            Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.
                Employees
                .OrderBy(e=> e.EmployeeId)
               .Select(e => new
               {
                   Name = $"{e.FirstName} {e.LastName} {e.MiddleName}",
                   e.JobTitle,
                   e.Salary
               });

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.Name} {e.JobTitle} {e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                }).Where(e => e.Salary > 50000)
                .OrderBy(x => x.FirstName);

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} - { e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
