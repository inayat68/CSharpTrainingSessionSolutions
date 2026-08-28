using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<User>? Users { get; set; }
}