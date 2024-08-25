using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC5
{
    internal class Student :User
    {
        public string Major {  get; set; }
        public double GPA { get; set; }

        public Student() { }

        public Student(int id , string name , string major , double gpa) 
        : base( id ,  name)
        {
            Major = major;
            GPA = gpa;
        }

        public void Display() {
            Console.WriteLine(ToString());
        }

        public string ToString() {
            return $"{base.ToString()},Major : {Major} , GPA : {GPA}";
        }
    }
}
