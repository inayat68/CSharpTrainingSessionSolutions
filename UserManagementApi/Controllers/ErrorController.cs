using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTOs;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    [HttpGet]
    public IActionResult Error()
    {
        return Problem(
            statusCode: 500,
            title: "Internal Server Error",
            detail: "An unexpected error occurred."
        );
    }
}