using DemoNotif.Controller;
using DemoNotif.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNoti.Service
{
    internal class DemoNotif
    {
        public static void Demo()
        {
            FileService fileSevice = new FileService();

            string studentFilePath = "Student.json";
            string teacherFilePath = "Teacher.json";

            List<Student> students = fileSevice.ReadFromJsonFile<Student>(studentFilePath);
            List<Teacher> teachers = fileSevice.ReadFromJsonFile<Teacher>(teacherFilePath);

            Course course1 = new Course("PRN212", "Program with C#");
            Course course2 = new Course("SWR302", "Software Requirment");
            Course course3 = new Course("SWT301", "Software Testing");
            Course course4 = new Course("SWP301", "Software Project");
            Course course5 = new Course("SWR302", "Software Requirment 2");
            Course course6 = new Course("SWT302", "Software Testing2");

            course1.SystemNotiStudent += CourseNoti;
            course1.SystemNotiTeacher += TeacherNoti;
            course2.SystemNotiStudent += CourseNoti;
            course2.SystemNotiTeacher += TeacherNoti;
            course3.SystemNotiStudent += CourseNoti;
            course3.SystemNotiTeacher += TeacherNoti;
            course4.SystemNotiStudent += CourseNoti;
            course4.SystemNotiTeacher += TeacherNoti;

            Console.WriteLine("Teacher 1 course 2 and 3 :");
            course2.addTeacher(teachers[0]);
            course3.addTeacher(teachers[0]);
            course2.Display();
            course3.Display();

            Console.WriteLine("Normal Course 1 :" );
            course1.addStudent(students[1]);
            course1.addStudent(students[2]);
            course1.addStudent(students[3]);
            course1.Display();

            Console.WriteLine("Course 1 Add student4 , Teacher 1 course 1:");
            course1.addStudent(students[0]);
            course1.addTeacher(teachers[0]);
            course1.Display();

            Console.WriteLine("Course 4 add teacher 2 , add 4 student");
            course4.addTeacher(teachers[1]);
            course4.addStudent(students[0]);
            course4.addStudent(students[1]);
            course4.addStudent(students[2]);
            course4.addStudent(students[3]);
            course4.Display();
            
        }

        public static void CourseNoti() {
            Console.WriteLine($"++++ Course  cant have more 3 student");
        }

        public static void TeacherNoti() {
            Console.WriteLine($"++++ Teacher  cant teach 3 course");
        }
    }
}
