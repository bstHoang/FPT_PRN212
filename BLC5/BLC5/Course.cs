using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC5
{
    internal class Course
    {
        public string Code {  get; set; }

        public string Title { get; set; }

        public int numberOfSlot {  get; set; }

        public Teacher teacher { get; set; }

        public List<Student> StudentsList { get; set; } = new List<Student>();
        public Course() { }

        public Course(string code , string title , int NumberOfslot , Teacher Teacher) { 
            Code = code;
            Title = title;
            numberOfSlot = NumberOfslot;
            teacher = Teacher;
        }

        public void AddStudent(Student student) { 
            StudentsList.Add(student);  
        }

        public void RemoveStudent(int studentId) {
            var student = StudentsList.FirstOrDefault(c => c.Id == studentId );
            if (student != null)
            {
                StudentsList.Remove(student);
            }
            else {
                Console.WriteLine("Cant find student");
            }
        }

        public void displayStudent() {
            Console.WriteLine($"Student int {Code}");
            foreach (var student in StudentsList) {
                student.Display();
            }
        }

        public void Display() { 
            Console.WriteLine(ToString());
            displayStudent();
        }

        public override string ToString()
        {
            return $"Code : {Code} ,Title : {Title}, Number Of Slot : {numberOfSlot},Teacher : {teacher.ToString()}";
        }
    }
}
