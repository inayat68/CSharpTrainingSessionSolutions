using UserManagementApi.DTOs;
using UserManagementApi.Models;

namespace UserManagementApi.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsers();

    Task<User?> GetById(int id);

    Task<List<UserDto?>> GetByManagerId(int id);

    Task<User?> GetByEmail(string email);

    Task Add(User user);

    Task Update(User user);

    Task Save();
}