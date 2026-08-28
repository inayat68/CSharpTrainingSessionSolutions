using System;
using System.Collections.Generic;
using System.Text;

namespace _60_JSON_FilesDempApp.Models
{

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public List<string> Skills { get; set; } = [];
    }
}
