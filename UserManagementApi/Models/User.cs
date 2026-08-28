using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementApi.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public int? ManagerId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public int? RoleId { get; set; }

    public DateTime JoiningDate { get; set; }

    public bool IsLoggedIn { get; set; }

    [ForeignKey("RoleId")]
    public Role? Role { get; set; }

    [ForeignKey("ManagerId")]
    public User? Manager { get; set; }

    public ICollection<User>? Employees { get; set; }

    public ICollection<TaskItem>? Tasks { get; set; }
}