using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double BaseSalary { get; set; }

        public Employee() { }

        public Employee(int id, string name, string position, double baseSalary)
        {
            Id = id;
            Name = name;
            Position = position;
            BaseSalary = baseSalary;
        }
    }
}
