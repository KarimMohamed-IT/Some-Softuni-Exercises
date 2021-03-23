using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            var context = new SoftUniContext();
            Console.WriteLine(GetEmployee147(context));

        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context
                .Employees
                .Where(x=> x.EmployeeId == 147)
                .Select(e => new
                {
                    FullName = e.FirstName + " " + e.LastName,
                    e.JobTitle,
                   ProjectNames =  e.EmployeesProjects.OrderBy(x=> x.Project.Name).Select(ep=> ep.Project.Name)
                }).First();
                
            var sb = new StringBuilder();
            sb.AppendLine($"{employee.FullName} - {employee.JobTitle}");
            foreach (var project in employee.ProjectNames)
            {
                sb.AppendLine(project);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context
           .Addresses
           .OrderByDescending(e => e.Employees.Count)
           .ThenBy(t => t.Town.Name)
           .ThenBy(ad => ad.AddressText)
           .Take(10)
           .Select(x => new
           {
               x.AddressText,
               TownName = x.Town.Name,
               EmployeeCount = x.Employees.Count
           }).ToList();

            var sb = new StringBuilder();
            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {

            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects.Any(x =>
                 x.Project.StartDate.Year >= 2001 &&
                    x.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    EmployeeFullName = e.FirstName + " " + e.LastName,
                    ManagerFullName = e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects
                   .Select(ep => new
                   {
                       ProjectName = ep.Project.Name,
                       StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),

                       EndDate = ep.Project.EndDate.HasValue ?
                      ep.Project
                      .EndDate
                      .Value
                      .ToString("M/d/yyyy h:mm:ss tt"
                      , CultureInfo.InvariantCulture)
                      : "not finished",
                   })
                }).ToList();


            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeFullName} – Manager: {employee.ManagerFullName}");
                foreach (var project in employee.Projects)
                {
                    sb.AppendLine($"––{project.ProjectName} – {project.StartDate} – {project.EndDate}");
                }
            }
            return sb.ToString().TrimEnd();
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownId = 4;
            context.Add(address);
            context.SaveChanges();

            var emp = context.
                  Employees
                  .FirstOrDefault(x => x.LastName == "Nakov");
            emp.Address = address;

            context.SaveChanges();
            var employees = context
                .Employees
                .Select(x => new
                {
                    x.Address.AddressText,
                    x.AddressId
                })
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .ToList();


            var sb = new StringBuilder();
            foreach (var empl in employees)
            {
                sb.AppendLine(empl.AddressText);
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.
                Employees
                .OrderBy(e => e.EmployeeId)
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

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary

                })
                .ToList()
                .OrderBy(s => s.Salary)
                .ThenByDescending(n => n.FirstName);

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName + " " + emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

