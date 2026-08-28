using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly UserService _userService;

    public DashboardController(
        UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult>
        Dashboard()
    {
        var email =
            User.FindFirstValue(
                ClaimTypes.Email);

        if (email == null)
            return Unauthorized();

        var user =
            await _userService
            .GetUserByEmail(email);

        if (user == null)
            return NotFound();

        return Ok(
            new
            {
                user.Id,
                user.Name,
                user.Email,
                user.JoiningDate,
                user.IsLoggedIn,
                Role =
                    user.Role?.Name
            });
    }
}