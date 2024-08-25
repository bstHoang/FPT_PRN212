// See https://aka.ms/new-console-template for more information
using ConfigFile2.Model;

Teacher teacher1 = new Teacher(1,"Hoang","Teach");
StudentPRN212 student1 = new StudentPRN212(2, "Cuong", "SE",5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0) ;
Course PRN212 = new Course("PRN212","Program with C#",teacher1) ;
PRN212.addStudent(student1);
PRN212.Display();

