using EmployeeManagement.Scheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Scheduler;

public static class DependencyInjection
{
    public static IServiceCollection AddScheduler(this IServiceCollection services)
    {
        // Register job so Hangfire can resolve it via DI
        services.AddScoped<EmployeeSyncJob>();

        return services;
    }
}
