using Microsoft.EntityFrameworkCore;
using UserManagementApi.Data;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

public class RoleService
{
    private readonly AppDbContext _db;

    public RoleService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Role>> GetRoles()
    {
        return await _db.Roles.ToListAsync();
    }

    public async Task<int?> GetRoleIdByName(string roleName)
    {
        var role = await _db.Roles
            .FirstOrDefaultAsync(r => r.Name == roleName);

        return role?.Id;
    }
}