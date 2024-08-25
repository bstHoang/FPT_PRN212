using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDeligate.Model
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double GPA { get; set; }
        public DateOnly DOB { get; set; }

        public Student(int id , string name , double gpa , DateOnly dob) {
            Id = id;
            Name = name;
            GPA = gpa;
            DOB = dob;
        }
        public static int CompareById(Student x, Student y) { 
            return x.Id.CompareTo(y.Id);
        }
        public override string ToString()
        {
            return $"ID : {Id} , Name : {Name} , GPA : {GPA} , DOB : {DOB}";
        }
    }
}
