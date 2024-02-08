using LINQ_Assignment_1;
using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// This program  uses LINQ   operations on collections of Employee, Department, and Project objects. 
/// The program performs various tasks such as grouping, aggregating, joining, filtering, and selecting data from these collections.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Initialize employees, departments, and projects
        List<Employee> employees = new List<Employee>
        {
            new Employee { EmployeeID = 1, FirstName = "Emma", LastName = "Watson", Salary = 10000, DepartmentID = 1 },
            new Employee { EmployeeID = 2, FirstName = "Daniel", LastName = "Rady", Salary = 20000, DepartmentID = 1 },
            new Employee { EmployeeID = 3, FirstName = "Michael", LastName = "JackSon", Salary = 5000, DepartmentID = 2 },
            new Employee { EmployeeID = 4, FirstName = "Ashley", LastName = "Burwell", Salary = 45000, DepartmentID = 2 },
            new Employee { EmployeeID = 5, FirstName = "Sissy", LastName = "Bryer", Salary = 50000, DepartmentID = 3 },
            new Employee { EmployeeID = 6, FirstName = "Lanie", LastName = "Churco", Salary = 97000, DepartmentID = 3 },
            new Employee { EmployeeID = 7, FirstName = "Taylor", LastName = "Swift", Salary = 72000, DepartmentID = 3 },
            new Employee { EmployeeID = 8, FirstName = "Selena", LastName = "Gomez", Salary = 8000, DepartmentID = 1 },
            new Employee { EmployeeID = 9, FirstName = "Ranbir", LastName = "Kapoor", Salary = 9000, DepartmentID = 1 },
            new Employee { EmployeeID = 10, FirstName = "David", LastName = "Potter", Salary = 3000, DepartmentID = 2 }
        };


        List<Department> departments = new List<Department>
        {
            new Department { DepartmentID = 1, DepartmentName = "Marketing" },
            new Department { DepartmentID = 2, DepartmentName = "Production" },
            new Department { DepartmentID = 3, DepartmentName = "Accounting" }
        };

        List<Project> projects = new List<Project>
        {
            new Project { ProjectID = 1, ProjectName = "Market Trends", DepartmentID = 1 },
            new Project { ProjectID = 2, ProjectName = "Manufacturing", DepartmentID = 2 },
            new Project { ProjectID = 3, ProjectName = "Data Security in Finance", DepartmentID = 3 },
            new Project { ProjectID = 4, ProjectName = "Media Selection", DepartmentID = 1 },
            new Project { ProjectID = 5, ProjectName = "Product Launch", DepartmentID = 2 }
        };

        // Group employees by their departments
        var employeesDepartment = employees.GroupBy(e => e.DepartmentID);

        //Calculate the average Salary of Department 
        // Calculate the average salary for each department
        var averageSalary = employees.GroupBy(e => e.DepartmentID)
                                                  .Select(g => new
                                                  {
                                                      DepartmentID = g.Key,
                                                      AverageSalary = g.Average(e => e.Salary)
                                                  });

        // Find the department with the highest total salary
        var HighestSalary = employees.GroupBy(e => e.DepartmentID)
                                                    .Select(g => new
                                                    {
                                                        DepartmentID = g.Key,
                                                        TotalSalary = g.Sum(e => e.Salary)
                                                    })
                                                    .OrderByDescending(x => x.TotalSalary)
                                                    .FirstOrDefault();

        // Group employees by the projects they are involved in
        var employeesProject = employees.Join(projects,
                                                 e => e.DepartmentID,
                                                 p => p.DepartmentID,
                                                 (e, p) => new { Employee = e, Project = p })
                                           .GroupBy(x => x.Project.ProjectID);

        // Calculate the total number of projects in each department
        var projectsDepartment = projects.GroupBy(p => p.DepartmentID)
                                            .Select(g => new
                                            {
                                                DepartmentID = g.Key,
                                                TotalProjects = g.Count()
                                            });

        //  Joins

        var joinEmployees = employees.Join(departments,
                                         e => e.DepartmentID,
                                         d => d.DepartmentID,
                                         (e, d) => new { Employee = e, Department = d })
                                   .Join(projects,
                                         ed => ed.Employee.DepartmentID,
                                         p => p.DepartmentID,
                                         (ed, p) => new
                                         {
                                             EmployeeName = ed.Employee.FirstName + " " + ed.Employee.LastName,
                                             Department = ed.Department.DepartmentName,
                                             Project = p.ProjectName
                                         });

        // Display the result of the joins
        Console.WriteLine("Employee Name\t\tDepartment\t\tProject");
        foreach (var item in joinEmployees)
        {
            Console.WriteLine($"{item.EmployeeName}\t\t{item.Department}\t\t{item.Project}");
        }

        //  Where and Select
        // Filter employees based on salary threshold
        var highestPaidEmployees = employees.Where(e => e.Salary > 70000);

        // Select FirstName, LastName of employees and ProjectName of projects
        var highSalary = highestPaidEmployees.Join(projects,
                                                  e => e.DepartmentID,
                                                  p => p.DepartmentID,
                                                  (e, p) => new
                                                  {
                                                      e.FirstName,
                                                      e.LastName,
                                                      p.ProjectName
                                                  });

        // Display selected names with theri project Name
        Console.WriteLine("\nEmployees with salary more than 70000:");
        Console.WriteLine("FirstName\tLastName\tProjectName");
        foreach (var item in highSalary)
        {
            Console.WriteLine($"{item.FirstName}\t\t{item.LastName}\t\t{item.ProjectName}");
        }

    }
}

 
