using System;
using System.Collections.Generic;
using System.Text;

namespace _056_PartialClassDemo.Models
{
    internal partial class Employee
    {
        public decimal Salary { get; set; }
        public void DisplaySalary()
        {
            Console.WriteLine($"Salary: {Salary:N2}");
        }
        public decimal CalculateAnnualSalary() { return Salary * 12; }
    }
}
