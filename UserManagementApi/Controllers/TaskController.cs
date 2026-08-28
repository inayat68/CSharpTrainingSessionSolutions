using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTOs;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(
        TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult>
        GetTasks()
    {
        var tasks =
            await _taskService.GetTasks();

        return Ok(tasks);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult>
        GetUserTasks(
        int userId)
    {
        var tasks =
            await _taskService
            .GetUserTasks(userId);

        return Ok(tasks);
    }

    [HttpPost]
    [Authorize(Roles =
        "Admin,Manager")]
    public async Task<IActionResult>
        AssignTask(
        TaskDto dto)
    {
        var task =
            new TaskItem
            {
                Title = dto.Title,
                Description =
                    dto.Description,
                AssignedBy =
                    dto.AssignedBy,
                Status =
                    dto.Status,
                CompletionDate =
                    dto.CompletionDate,
                FilePath =
                    dto.FilePath,
                IsAssigned =
                    dto.IsAssigned,
                UserId =
                    dto.UserId,
                CreatedAt =
                    DateTime.Now
            };

        await _taskService.AddTask(task);

        return Ok(
            new
            {
                Success = true,
                Message =
                    "Task Assigned"
            });
    }
}