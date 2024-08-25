using PE_SU24_Q1;

internal class Program { 
private static void Main(string[] args)
    {
        Console.WriteLine("Requirment 1 :");
        Employee s = new Employee(1, "Nguyen Van A", new DateOnly(1999, 10, 20), false);
        Console.WriteLine("You have entered");
        DisplayFullInfoOfEmployee(s);

        Console.WriteLine(Environment.NewLine + "-------------");
        Console.WriteLine("Requirement 2:");
        Department<Employee> department = new Department<Employee>("Accounting Department");
        department.Add(new Employee(2, "Nguyen Van B", new DateOnly(1999, 10, 20), false));
        department.Add(new Employee(3, "Nguyen Van C", new DateOnly(1989, 11, 15), true));
        department.Add(new Employee(4, "Nguyen Van D", new DateOnly(2000, 4, 2), true));
        department.Display(DisplayFullInfoOfEmployee);

        Console.WriteLine(Environment.NewLine + "-------------");
        Console.WriteLine("Requirement 3:");
        Employee employee = new Employee(3, "Nguyen Van C", new DateOnly(1989, 11, 15), true);
        if (department.Contains(employee))
            Console.WriteLine("The employee you are looking for belongs to the department.");
        else Console.WriteLine("The employee you are looking for does not belong to the department.");


        Console.WriteLine(Environment.NewLine + "-------------");
        Console.WriteLine("Requirement 4:");
        department.Display(DisplayBriefInfoOfEmployee);
    }

    private static void DisplayFullInfoOfEmployee(Employee employee)
    {
        Console.WriteLine($"{employee.Id} - {employee.Name} - {employee.Dob.ToLongDateString()} - IsMale: {employee.IsMale}");
    }

    private static void DisplayBriefInfoOfEmployee(Employee employee)
    {
        Console.WriteLine($"{employee.Id} - {employee.Name}");
    }
}