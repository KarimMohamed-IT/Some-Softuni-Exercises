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
            Console.WriteLine(RemoveTown(context));

        }

        public static string RemoveTown(SoftUniContext context)
        {
            var town = context
                .Towns
                .Where(x => x.Name == "Seattle").FirstOrDefault();

            var addressesToDel = context
                .Addresses
                .Where(a => a.TownId == town.TownId);

            int addressesCount = addressesToDel.Count();

            var employees = context
                 .Employees
                 .Where(e => addressesToDel.Any(a => a.AddressId == e.AddressId));

            foreach (var e in employees)
            {
                e.AddressId = null;
            }

            foreach (var a in addressesToDel)
            {
                context.Remove(a);
            }

            context.Remove(town);
            context.SaveChanges();
            return $"{addressesCount} addresses in {town.Name} were deleted";
        }
        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectsOnEmployees = context
                .EmployeesProjects
                .Where(x => x.ProjectId == 2);
            foreach (var project in projectsOnEmployees)
            {
                context.Remove(project);
            }
            context.SaveChanges();

            var removingProject = context.
                Projects.Find(2);

            context.Remove(removingProject);
            context.SaveChanges();

            var projects = context
                .Projects
                .Take(10)
                .Select(x => x.Name);
            

            var sb = new StringBuilder();
            foreach (var project in projects)
            {
                sb.AppendLine(project);
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(x => x.FirstName.ToLower().StartsWith("sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                }).OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(e =>
                e.Department.Name == "Engineering" ||
                e.Department.Name == "Tool Design" ||
                e.Department.Name == "Marketing" ||
                e.Department.Name == "Information Services").OrderBy(e=> e.FirstName).ThenBy(e=> e.LastName);
            foreach (var employeeSalary in employees)
            {
                employeeSalary.Salary += employeeSalary.Salary * 0.12m;
            }
            context.SaveChanges();

            var sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context
                .Projects
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                  StartDate =  p.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)
                }).OrderBy(x => x.Name)
                .ToList();

            var sb = new StringBuilder();
            foreach (var project in projects)
            {
                sb.AppendLine(project.Name);
                sb.AppendLine(project.Description);
                sb.AppendLine(project.StartDate);
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context
                .Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    DepartamentName = x.Name,
                    ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Employee = x.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    }).OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName).ToList()
                }).ToList();

            var sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartamentName} - {department.ManagerName}");
                foreach (var employee in department.Employee)
                {
                    sb.AppendLine($"{employee.FirstName + " " + employee.LastName} - {employee.JobTitle}");
                }
            }
            return sb.ToString().TrimEnd();
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

