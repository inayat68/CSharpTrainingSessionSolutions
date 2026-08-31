using System;
using System.Collections.Generic;
using System.Text;

namespace _056_PartialClassDemo.Models
{
    internal partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public void DisplayBasicInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}"); 
            Console.WriteLine($"Department: {Department}");
        }
    }
}
