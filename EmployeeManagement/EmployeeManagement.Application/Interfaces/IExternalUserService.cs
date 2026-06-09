using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Interfaces;

public interface IExternalUserService
{
    Task<IEnumerable<ExternalUserDto>> GetAllAsync();
    Task<ExternalUserDto?> GetByIdAsync(int id);
    Task<bool> UserExistsAsync(string email);
    Task SyncUsersAsync();
}
