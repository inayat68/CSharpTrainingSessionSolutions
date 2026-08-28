using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementApi.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string AssignedBy { get; set; } = string.Empty;

    public int Status { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string? FilePath { get; set; }

    public bool IsAssigned { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}