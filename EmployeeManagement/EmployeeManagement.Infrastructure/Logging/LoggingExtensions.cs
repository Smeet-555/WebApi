using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Infrastructure.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddInfrastructureLogging(this IServiceCollection services)
    {

        services.AddLogging();
        return services;
    }
}
