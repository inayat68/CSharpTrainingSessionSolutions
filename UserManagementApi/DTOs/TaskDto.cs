namespace UserManagementApi.DTOs;

public class TaskDto
{
    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string AssignedBy { get; set; } = "";

    public int Status { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string? FilePath { get; set; }

    public bool IsAssigned { get; set; }

    public int UserId { get; set; }
}