using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.ExternalApis;
using EmployeeManagement.Infrastructure.Options;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind and validate ConnectionStringOptions at startup
        services.AddOptions<ConnectionStringOptions>()
            .Bind(configuration.GetSection(ConnectionStringOptions.SectionName))
            .ValidateDataAnnotations()        // fails fast if DefaultConnection is missing
            .ValidateOnStart();

        // Register EF Core — read connection string via options
        var connectionString = configuration
            .GetSection(ConnectionStringOptions.SectionName)
            .Get<ConnectionStringOptions>()!
            .DefaultConnection;

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register repository
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        // Register external user service with typed HttpClient
        services.AddHttpClient<IExternalUserService, ExternalUserService>();

        return services;
    }
}
