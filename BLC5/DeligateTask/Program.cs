using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DeligateTask.Model;

namespace DeligateTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = "data.json";
            var courses = ReadCoursesFromJson(jsonFilePath);

            foreach (var course in courses)
            {
                course.Display();
                Console.WriteLine("----------------------");
            }

            Console.WriteLine("Students who passed:");
            foreach (var course in courses)
            {
                var passingStudents = course.Students.Where(IsPass).ToList();
                foreach (var student in passingStudents)
                {
                    Console.WriteLine($"{student.Name} passed the course: {course.Title}");
                }
            }
        }

        static List<Course> ReadCoursesFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Course>>(json);
        }

        static bool IsPass(Student student)
        {
            var weightedSum = student.Grades.Sum(g => g.Mark * g.Rate);
            var totalRate = student.Grades.Sum(g => g.Rate);

            var weightedAverage = weightedSum / totalRate;

            return weightedAverage >= 5 && student.Grades.All(g => g.Mark >= 2);
        }
    }
}
