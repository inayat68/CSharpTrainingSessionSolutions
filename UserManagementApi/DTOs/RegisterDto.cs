namespace UserManagementApi.DTOs;

public class RegisterDto
{
    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public DateTime JoiningDate { get; set; }

    public int RoleId { get; set; }

    public int? ManagerId { get; set; }
}