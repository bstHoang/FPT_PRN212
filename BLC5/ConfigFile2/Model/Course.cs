using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile2.Model
{
    internal class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }    
        public Teacher Teacher { get; set; }

        public List<StudentPRN212> studentList { get; set; } = new List<StudentPRN212>();

        public Course() { }

        public Course(string code , string name , Teacher teacher )  {
            Code = code;
            Name = name;
            Teacher = teacher;
        }

        public void addStudent(StudentPRN212 student) { 
            studentList.Add(student);
        }

        public void Display() {
            Console.WriteLine(ToString());
            foreach (StudentPRN212 student in studentList) { 
                Console.WriteLine (student.ToString());
            }
        }
        public override string ToString()
        {
            return $"Code : {Code} , Name : {Name} , Teacher infor :  {Teacher.ToString()} ";
        }

    }
}
