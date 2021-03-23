using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            var context = new SoftUniContext();
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));

        }
        //public static string GetEmployeesFullInformation(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    var employees = context.
        //        Employees
        //        .OrderBy(e => e.EmployeeId)
        //       .Select(e => new
        //       {
        //           Name = $"{e.FirstName} {e.LastName} {e.MiddleName}",
        //           e.JobTitle,
        //           e.Salary
        //       });

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.Name} {e.JobTitle} {e.Salary:f2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}
        //public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    var employees = context
        //        .Employees
        //        .Select(e => new
        //        {
        //            e.FirstName,
        //            e.Salary
        //        }).Where(e => e.Salary > 50000)
        //        .OrderBy(x => x.FirstName);

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.FirstName} - { e.Salary:f2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                   e.FirstName ,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary

                })
                .ToList()
                .OrderBy(s=> s.Salary)
                .ThenByDescending(n => n.FirstName);

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName + " " + emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
            }

            return sb.ToString().Trim();
        }

        //stringbuilder sb = new stringbuilder();

        //   return sb.tostring().trimend();

    }
}

