using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.Infrastructure.ExternalApis;

public class ExternalUserService : IExternalUserService
{
    private readonly HttpClient _httpClient;
    private readonly ExternalApiOptions _options;
    private readonly ILogger<ExternalUserService> _logger;

    public ExternalUserService(
        HttpClient httpClient,
        IOptions<ExternalApiOptions> options,
        ILogger<ExternalUserService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/users?email={email}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check if user exists for email {Email}", email);
            return false;
        }
    }

    public async Task SyncUsersAsync()
    {
        try
        {
            _logger.LogInformation("Starting external user sync...");

            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/users");
            response.EnsureSuccessStatusCode();

            // TODO: deserialize and sync users into the local database

            _logger.LogInformation("External user sync completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "External user sync failed.");
        }
    }
}
