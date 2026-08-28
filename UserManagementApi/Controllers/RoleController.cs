using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(
        RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult>
        GetRoles()
    {
        var roles =
            await _roleService.GetRoles();

        return Ok(roles);
    }
}