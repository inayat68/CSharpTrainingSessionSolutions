using Microsoft.EntityFrameworkCore;
using UserManagementApi.Data;
using UserManagementApi.DTOs;
using UserManagementApi.Models;

namespace UserManagementApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _db.Users
            .Include(x => x.Role)
            .Include(x => x.Manager)
            .ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _db.Users
            .Include(x => x.Role)
            .Include(x => x.Manager)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<UserDto>> GetByManagerId(int id)
    {
        return await _db.Users
            .Where(x => x.ManagerId == id)
            .Include(x => x.Role)
            .Include(x => x.Manager)
            .Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                RoleName = x.Role.Name,
                ManagerName = x.Manager != null ? x.Manager.Name : null
            })
            .ToListAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _db.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task Add(User user)
    {
        await _db.Users.AddAsync(user);
    }

    public Task Update(User user)
    {
        _db.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}