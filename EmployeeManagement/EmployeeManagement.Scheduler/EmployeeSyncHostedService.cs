using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Scheduler;

// Runs EmployeeSyncJob on a fixed interval using IHostedService
public class EmployeeSyncHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<EmployeeSyncHostedService> _logger;

    // Run every 60 minutes
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(60);

    public EmployeeSyncHostedService(IServiceScopeFactory scopeFactory, ILogger<EmployeeSyncHostedService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EmployeeSyncHostedService is running.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await RunJobAsync();
            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task RunJobAsync()
    {
        try
        {
            // Create a new scope per execution — EmployeeSyncJob uses scoped services
            using var scope = _scopeFactory.CreateScope();
            var job = scope.ServiceProvider.GetRequiredService<Jobs.EmployeeSyncJob>();
            await job.ExecuteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "EmployeeSyncJob failed.");
        }
    }
}
