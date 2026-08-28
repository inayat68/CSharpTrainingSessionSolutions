using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(
        UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult>
        GetUsers()
    {
        var users =
            await _userService.GetUsers();

        return Ok(users);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult>
        GetUserByEmail(
        string email)
    {
        var user =
            await _userService
            .GetUserByEmail(email);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("manager/{managerId}")]
    public async Task<IActionResult> GetUserByManagerId(int managerId)
    {
        var user = await _userService.GetUserByManagerId(managerId);

        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user);
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(
            new
            {
                Message =
                "Protected API Working"
            });
    }
}