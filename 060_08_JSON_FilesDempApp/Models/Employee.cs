using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace _60_JSON_FilesDempApp.Models
{

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public List<string> Skills { get; set; } = [];

        public int Age { get; set; } = 40;
    }
}
