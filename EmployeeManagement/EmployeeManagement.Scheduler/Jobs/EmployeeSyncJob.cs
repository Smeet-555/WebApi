using EmployeeManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Scheduler.Jobs;

// Background job that syncs employees from the external API on a schedule
public class EmployeeSyncJob
{
    private readonly IExternalUserService _externalUserService;
    private readonly ILogger<EmployeeSyncJob> _logger;

    public EmployeeSyncJob(IExternalUserService externalUserService, ILogger<EmployeeSyncJob> logger)
    {
        _externalUserService = externalUserService;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("EmployeeSyncJob started at {Time}", DateTime.UtcNow);

        await _externalUserService.SyncUsersAsync();

        _logger.LogInformation("EmployeeSyncJob completed at {Time}", DateTime.UtcNow);
    }
}
