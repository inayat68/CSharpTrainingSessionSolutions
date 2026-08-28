using System.ComponentModel.DataAnnotations;

namespace CustomerManagementWebApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = "";

        [StringLength(30)]
        public string Phone { get; set; } = "";
    }
}