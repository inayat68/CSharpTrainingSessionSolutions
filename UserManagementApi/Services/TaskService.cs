using Microsoft.EntityFrameworkCore;
using UserManagementApi.Data;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

public class TaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskItem>> GetTasks()
    {
        return await _db.Tasks
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<List<TaskItem>>
        GetUserTasks(int userId)
    {
        return await _db.Tasks
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task AddTask(TaskItem task)
    {
        await _db.Tasks.AddAsync(task);

        await _db.SaveChangesAsync();
    }
}