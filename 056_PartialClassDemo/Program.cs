using _056_PartialClassDemo.Models;

namespace _056_PartialClassDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Program.cs

            Employee employee = new Employee
            {
                Id = 101,
                Name = "Ali",
                Department = "IT",
                Salary = 150000
            };

            employee.DisplayBasicInfo();

            Console.WriteLine($"Monthly Salary: {employee.Salary:N2}");
            Console.WriteLine($"Annual Salary: {employee.CalculateAnnualSalary():N2}");
        }
    }
}
