using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Models
{
    public class Test
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }

        [RegularExpression(
            @"^\d{3}-\d{3}-\d{4}$",
        ErrorMessage = "Phone number must be in the format ###-###-####")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Username cannot exceed 10 characters")]
        [RegularExpression(
        @"^[A-Za-z]+[0-9]+$",
        ErrorMessage = "Username must start with letters followed by numbers only")]
        public string Username { get; set; } = string.Empty;

        // Password
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8,
            ErrorMessage = "Password must contain at least 8 characters")]
        [MaxLength(12,
            ErrorMessage = "Password must contain max 12 characters")]
        public string Password { get; set; } = string.Empty;


        // Compare two properties
        [Compare("Password",
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // DateTime with display format
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime LastLogin { get; set; }

    }
}
