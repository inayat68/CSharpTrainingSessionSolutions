namespace UserManagementApi.DTOs;

public class UpdateTaskStatusDto
{
    public int TaskId { get; set; }

    public int Status { get; set; }

    public DateTime? CompletionDate { get; set; }
}