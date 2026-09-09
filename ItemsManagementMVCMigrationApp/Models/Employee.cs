using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string EmailAddress { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        // Foreign Key
        public int DepartmentId { get; set; }

        public DateTime CreateDate { get; set; }

        // Navigation Property
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;
    }
}
