using EmployeeManagement.Scheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Scheduler;

public static class DependencyInjection
{
    public static IServiceCollection AddScheduler(this IServiceCollection services)
    {

        services.AddScoped<EmployeeSyncJob>();

        services.AddHostedService<EmployeeSyncHostedService>();

        return services;
    }
}
