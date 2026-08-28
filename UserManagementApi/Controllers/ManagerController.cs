using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize(Roles = "Manager")]
[Route("api/manager")]
public class ManagerController : ControllerBase
{
    private readonly UserService _userService;

    public ManagerController(
        UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("employees")]
    public async Task<IActionResult> Employees()
    {
        var users = await _userService.GetUsers();

        var employees = users.Where(x =>
                x.Role?.Name ==
                "Employee");

        return Ok(employees);
    }
}