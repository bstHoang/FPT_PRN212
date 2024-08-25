using Question1;
using System;
using System.Collections.Generic;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee(1, "HungNH", "Saler", 1000);
            Console.WriteLine("Testcase 1:");
            e.Display(x => (x.Position.Equals("Manager") ? x.BaseSalary * 15 : x.BaseSalary * 13));

            Console.WriteLine("\nTestcase 2:");
            List<Employee> employees = new List<Employee>
            {
                new Employee(1, "TrangNT", "Manager", 1500),
                new Employee(2, "HangKT", "Saler", 800),
                new Employee(1, "DungNH", "Saler", 900)
            };
            double sum = Employee.GetSumOfSalary(employees,
                (x => (x.Position.Equals("Manager") ? x.BaseSalary * 16 : x.BaseSalary * 14))
                );
            Console.WriteLine($"Sum of salary:{sum}");
        }

    }
}
