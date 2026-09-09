using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        // One Department can have many Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
