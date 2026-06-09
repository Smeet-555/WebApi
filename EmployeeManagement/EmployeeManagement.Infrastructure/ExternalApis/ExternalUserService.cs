using System.Net.Http.Json;
using EmployeeManagement.Application.DTOs;
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

        if (!string.IsNullOrEmpty(_options.ApiKey))
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _options.ApiKey);
    }

    public async Task<IEnumerable<ExternalUserDto>> GetAllAsync()
    {
        try
        {
            var users = await _httpClient.GetFromJsonAsync<List<JsonPlaceholderUser>>(
                $"{_options.BaseUrl}/users");

            if (users is null) return [];

            _logger.LogInformation("GetAllAsync: Fetched {Count} users from external API.", users.Count);
            return users.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAllAsync failed.");
            return [];
        }
    }

    public async Task<ExternalUserDto?> GetByIdAsync(int id)
    {
        try
        {
            var user = await _httpClient.GetFromJsonAsync<JsonPlaceholderUser>(
                $"{_options.BaseUrl}/users/{id}");

            if (user is null) return null;

            _logger.LogInformation("GetByIdAsync: Fetched user Id={Id}", id);
            return MapToDto(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetByIdAsync failed for Id={Id}", id);
            return null;
        }
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        try
        {
            var users = await _httpClient.GetFromJsonAsync<List<JsonPlaceholderUser>>(
                $"{_options.BaseUrl}/users");

            var exists = users?.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ?? false;
            _logger.LogInformation("UserExistsAsync: email={Email} exists={Exists}", email, exists);
            return exists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UserExistsAsync failed for email={Email}", email);
            return false;
        }
    }

    public async Task SyncUsersAsync()
    {
        try
        {
            _logger.LogInformation("SyncUsersAsync: starting...");
            var users = await GetAllAsync();
            _logger.LogInformation("SyncUsersAsync: completed. {Count} users synced.", users.Count());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SyncUsersAsync failed.");
        }
    }

    // Map internal JSON shape to the public DTO
    private static ExternalUserDto MapToDto(JsonPlaceholderUser u) => new()
    {
        Id = u.Id,
        Name = u.Name,
        Username = u.Username,
        Email = u.Email,
        Phone = u.Phone,
        Website = u.Website,
        Company = u.Company?.Name ?? string.Empty
    };
}

internal class JsonPlaceholderUser
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public JsonPlaceholderCompany? Company { get; set; }
}

internal class JsonPlaceholderCompany
{
    public string Name { get; set; } = string.Empty;
}
