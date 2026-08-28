using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTOs;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    public TestController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var tasks = "This is sample test";
           
        return Ok(tasks);
    }

}