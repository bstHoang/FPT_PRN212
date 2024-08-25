using System;
using System.Collections.Generic;

namespace DeligateTask.Model
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Grade> Grades { get; set; } = new List<Grade>();

        public Student() { }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddMark(Grade grade)
        {
            Grades.Add(grade);
        }

        public void Display()
        {
            Console.WriteLine(ToString());
            foreach (Grade grade in Grades)
            {
                Console.WriteLine(grade.ToString());
            }
        }

        public override string ToString()
        {
            return $"Id : {Id} , Name : {Name}";
        }
    }
}
