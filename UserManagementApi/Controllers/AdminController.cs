using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TaskService _taskService;

    public AdminController(
        UserService userService,
        TaskService taskService)
    {
        _userService = userService;
        _taskService = taskService;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> Statistics()
    {
        var users = await _userService.GetUsers();

        var tasks = await _taskService.GetTasks();

        return Ok(new
            {
                TotalUsers =
                    users.Count,

                TotalManagers =
                    users.Count(x =>
                        x.Role?.Name ==
                        "Manager"),

                TotalEmployees =
                    users.Count(x =>
                        x.Role?.Name ==
                        "Employee"),

                TotalTasks =
                    tasks.Count,

                CompletedTasks =
                    tasks.Count(x =>
                        x.Status == 100)
            });
    }
}