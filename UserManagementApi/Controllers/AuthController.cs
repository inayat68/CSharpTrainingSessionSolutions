using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTOs;
using UserManagementApi.Helpers;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly PasswordService _passwordService;
    private readonly JwtService _jwtService;

    public AuthController(
        UserService userService,
        RoleService roleService,
        PasswordService passwordService,
        JwtService jwtService)
    {
        _userService = userService;
        _roleService = roleService;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existing = await _userService.GetUserByEmail(dto.Email);

        if (existing != null)
        {
            return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Email already exists"
                });
        }
        //var roleId = await _roleService.GetRoleIdByName(dto.RoleName);
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password =
                _passwordService.HashPassword(
                    dto.Password),
            JoiningDate = dto.JoiningDate,
            RoleId = dto.RoleId,
            ManagerId = dto.ManagerId
        };
        await _userService.AddUser(user);

        return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User registered successfully"
            });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userService.GetUserByEmail(dto.Email);
        if (user == null)
        {
            return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid Email"
                });
        }
        if (!_passwordService.VerifyPassword(dto.Password, user.Password))
        {
            return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid Password"
                });
        }
        user.IsLoggedIn = true;       await _userService.UpdateUser(user);  var token = _jwtService.GenerateToken(user);
        return Ok(new
            {
                Success = true,
                Message = "Login Successful",
                Token = token,
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    Role = user.Role?.Name
                }
            });
    }

    [HttpPost("change-password")]
    public async Task<IActionResult>
        ChangePassword(
        ChangePasswordDto dto)
    {
        var user =
            await _userService
            .GetUserByEmail(dto.Email);

        if (user == null)
        {
            return NotFound(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "User not found"
                });
        }

        if (!_passwordService.VerifyPassword(
            dto.OldPassword,
            user.Password))
        {
            return BadRequest(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "Old password incorrect"
                });
        }

        user.Password =
            _passwordService.HashPassword(
                dto.NewPassword);

        await _userService.UpdateUser(user);

        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = "Password Changed"
            });
    }
}