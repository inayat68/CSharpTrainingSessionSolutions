using UserManagementApi.DTOs;
using UserManagementApi.Models;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _repo.GetUsers();
    }

    public async Task<User?> GetUserByEmail(
        string email)
    {
        return await _repo.GetByEmail(email);
    }

    public async Task<List<UserDto?>> GetUserByManagerId(
        int id)
    {
        return await _repo.GetByManagerId(id);
    }

    public async Task AddUser(User user)
    {
        await _repo.Add(user);
        await _repo.Save();
    }

    public async Task UpdateUser(User user)
    {
        await _repo.Update(user);
        await _repo.Save();
    }
}