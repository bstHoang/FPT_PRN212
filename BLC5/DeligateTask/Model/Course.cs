using System;
using System.Collections.Generic;

namespace DeligateTask.Model
{
    internal class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public Course() { }

        public Course(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void Display()
        {
            Console.WriteLine(ToString());
            foreach (Student student in Students)
            {
                student.Display();
            }
        }

        public override string ToString()
        {
            return $"ID : {Id} , Title : {Title}";
        }
    }
}
