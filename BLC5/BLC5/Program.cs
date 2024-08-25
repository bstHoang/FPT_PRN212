// See https://aka.ms/new-console-template for more information
using BLC5;

Teacher teacher = new Teacher(1, "Hoang", "SE", 50000);

Course PRN212 = new Course("PRN212" ,"Program with .net", 20, teacher);

Student student1 = new Student(1, "Cuong", "SE", 3.5);
Student student2 = new Student(2, "Phuong", "SE", 3.7);
Student student3 = new Student(3, "Trong", "SE", 3.8);

PRN212.AddStudent(student1);
PRN212.AddStudent(student2);
PRN212.AddStudent(student3);

PRN212.Display();

Console.WriteLine("\nRemoving student with ID 2 :");
PRN212.RemoveStudent(2);
Console.WriteLine("After delete student : ");
PRN212.Display();

Console.ReadLine();

