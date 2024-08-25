using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_SU24_Q1
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Dob { get; set; }
        public bool IsMale { get; set; }

        public Employee(int id, string name, DateOnly dob, bool isMale)
        {
            Id = id;
            Name = name;
            Dob = dob;
            IsMale = isMale;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Employee))
                return false;

            Employee emp = (Employee)obj;
            return Id == emp.Id && Name == emp.Name && Dob == emp.Dob && IsMale == emp.IsMale;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
