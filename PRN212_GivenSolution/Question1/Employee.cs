namespace Question1
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double BaseSalary { get; set; }

        public Employee(int id, string name, string position, double baseSalary)
        {
            Id = id;
            Name = name;
            Position = position;
            BaseSalary = baseSalary;
        }

        // Phương thức để hiển thị lương theo công thức tính toán truyền vào
        public void Display(Func<Employee, double> calculateSalary)
        {
            double salary = calculateSalary(this);
            Console.WriteLine($"Employee: {Name}, Position: {Position}, Calculated Salary: {salary}");
        }

        // Phương thức tính tổng lương cho danh sách nhân viên
        public static double GetSumOfSalary(List<Employee> employees, Func<Employee, double> calculateSalary)
        {
            double sum = 0;
            foreach (var employee in employees)
            {
                sum += calculateSalary(employee);
            }
            return sum;
        }
    }
}
