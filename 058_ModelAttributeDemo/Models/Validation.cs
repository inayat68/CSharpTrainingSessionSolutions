using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace _058_ModelAttributeDemo.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateFormatAttribute : Attribute
    {
        public string Format { get; }

        public DateFormatAttribute(string format)
        {
            Format = format;
        }
    }




    public class Employee
    {
        [DateFormatAttribute("MM/dd/yyyy")]
        public DateTime JoiningDate { get; set; }

        [DateFormatAttribute("MM/dd/yyyy")]
        public DateTime LeavingDate { get; set; }
    }




    public static class EmployeeValidator
    {
        public static bool Validate(Employee employee)
        {
            bool isValid = true;

            Type employeeType = typeof(Employee);

            // Get JoiningDate property
            PropertyInfo joiningProperty = employeeType.GetProperty(nameof(Employee.JoiningDate))!;

            // Get JoiningDateFormat attribute
            DateFormatAttribute? joiningAttribute = joiningProperty.GetCustomAttribute<DateFormatAttribute>();

            if (joiningAttribute != null)
            {
                string joiningDate = employee.JoiningDate.ToString(joiningAttribute.Format);

                Console.WriteLine($"Joining Date  : {joiningDate}");
                Console.WriteLine($"Joining Format: {joiningAttribute.Format}");
            }

            // Get LeavingDate property
            PropertyInfo leavingProperty = employeeType.GetProperty(nameof(Employee.LeavingDate))!;

            // Get LeavingDateFormat attribute
            DateFormatAttribute? leavingAttribute = leavingProperty.GetCustomAttribute<DateFormatAttribute>();

            if (leavingAttribute != null)
            {
                string leavingDate =
                    employee.LeavingDate.ToString(leavingAttribute.Format);

                Console.WriteLine($"Leaving Date   : {leavingDate}");
                Console.WriteLine($"Leaving Format : {leavingAttribute.Format}");
            }

            // Validate LeavingDate > JoiningDate
            if (employee.LeavingDate <= employee.JoiningDate)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: LeavingDate must be greater than JoiningDate.");

                isValid = false;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("OK: LeavingDate is greater than JoiningDate.");
            }

            return isValid;
        }
    }
}
