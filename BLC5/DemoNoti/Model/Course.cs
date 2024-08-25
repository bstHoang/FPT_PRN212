using DemoNotif.Deligate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotif.Model
{
    internal class Course
    {
        public string Code { get; set; }
        public string Title {  get; set; }

        public Teacher Teacher { get; set; }
        public List<Student> studentList { get; set; } = new List<Student> { };
        private static Dictionary<Teacher, int> teacherCourseCount = new Dictionary<Teacher, int>();


        private SystemNoti systemNotiStudent;
        private SystemNoti systemNotiTeacher;
        public Course() { }

        public Course (string code, string title)
        {
            Code = code;
            Title = title;
        }

        public event SystemNoti SystemNotiStudent { 
            add { systemNotiStudent += value; }
            remove { systemNotiStudent -= value; }
        }
        public event SystemNoti SystemNotiTeacher
        {
            add { systemNotiTeacher += value; }
            remove { systemNotiTeacher -= value; }
        }

        public void addStudent(Student student)
        {
            studentList.Add(student);
            if (studentList.Count > 3) systemNotiStudent();
        }
        public void addTeacher(Teacher teacher)
        {
            Teacher = teacher;

            if (teacherCourseCount.ContainsKey(teacher))
            {
                teacherCourseCount[teacher]++;
            }
            else
            {
                teacherCourseCount[teacher] = 1;
            }

            if (teacherCourseCount[teacher] > 2) systemNotiTeacher();
        }

        public void Display() {
            Console.WriteLine(ToString());
            foreach(Student student in studentList) {
                Console.WriteLine(student.ToString());
            }    
            Console.WriteLine("--------------");
        }
        public override string ToString()
        {
            string teacherInfo = Teacher != null
                ? $"Teacher information : {Teacher.ToString()}"
                : "No teacher assigned";
            return $"Code : {Code}, Title : {Title}, {teacherInfo}";
        }
    }
}
