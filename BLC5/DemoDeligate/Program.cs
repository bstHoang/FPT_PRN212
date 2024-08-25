using DemoDeligate.Model;

namespace DemoDeligate {
    class Program {
        private static void Main(string[] args)
        {
            //DemoBasicDeligate();
            Course course = new Course(1);
            Student student1 = new Student(4, "Hoang", 3.94, DateOnly.FromDateTime(DateTime.Now));
            Student student2 = new Student(2, "Phuong", 3.75, DateOnly.FromDateTime(DateTime.Now));
            course.addStudent(student1);
            course.addStudent(student2);
            course.Display();
        }
        public static int Add(int x,int y) {
            Console.WriteLine("Function add :");
            return x + y;
        }

        public static int Sub(int x,int y)
        {
            Console.WriteLine("Function Sub : ");
            return x - y;
        }

        public static void DemoBasicDeligate() {
            Caculation c;
            c = new Caculation(Add);
            Console.WriteLine(c(5, 3));
        }
    }
}