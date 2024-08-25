using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDeligate.Model
{
    internal class Course
    {
        public int Id { get; set; }
        public List<Student> StudentList { get; set; } = new List<Student>();
        public Course(int id) {
            Id = id;
        }

        public void addStudent(Student student)
        {
            StudentList.Add(student); 
        }
        public void Display() {
            Console.WriteLine($"ID course : {Id}");
            StudentList.Sort(Student.CompareById);
            Console.WriteLine("Create new func sort ID DESC");
            foreach ( var student in StudentList ) { 
                Console.WriteLine( student.ToString());
            }
            StudentList = StudentList.OrderByDescending(s => s.GPA).ToList();
            Console.WriteLine("LinQ sort GPA DESC");
            foreach (var student in StudentList)
            {   
                Console.WriteLine(student.ToString());
            }
            Console.WriteLine("Sort name by anonymous funtion (deligate):");
            StudentList.Sort(
                delegate (Student x , Student y) { 
                    return x.Name.CompareTo(y.Name);
                }
                );
            foreach (var Student in StudentList) {
                Console.WriteLine(Student.ToString());
            }
            Console.WriteLine("Sort name bt lambda Expression :");
            StudentList.Sort((x, y) => x.Name.CompareTo(y.Name));
            foreach (var Student in StudentList)
            {
                Console.WriteLine(Student.ToString());
            }
        }
    }
}
