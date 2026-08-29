using System.ComponentModel.DataAnnotations;

namespace SerilogWebApiDemo.Models
{
    public class EmployeeRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

}
