using System.Collections.Generic;

namespace Project.Model
{
    public class Course
    {
        public string CourseID { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
