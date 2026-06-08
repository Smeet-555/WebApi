namespace EmployeeManagement.Application.Interfaces;

// Contract for fetching user data from an external API
public interface IExternalUserService
{
    Task<bool> UserExistsAsync(string email);
    Task SyncUsersAsync();
}
