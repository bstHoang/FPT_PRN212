using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC5
{
    internal class Teacher : User
    {
        private string Skill { get; set; }
        private double Salary { get; set; }

        public Teacher() { }

        public Teacher(int id, string name, string skill, double salary) : base(id, name) {
            Skill = skill;
            Salary = salary;
        }
        public void Display() {
            Console.WriteLine(ToString());
        }

        public override String ToString() {
            return $"{base.ToString()},Skill : {Skill} ,Salary : {Salary}";
        }
    }
}
