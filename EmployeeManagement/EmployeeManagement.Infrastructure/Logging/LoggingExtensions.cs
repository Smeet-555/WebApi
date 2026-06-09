using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddInfrastructureLogging(this IServiceCollection services)
    {
        services.AddLogging();
        return services;
    }
}
