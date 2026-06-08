using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Options;
using EmployeeManagement.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Register application services
        services.AddScoped<IEmployeeService, EmployeeService>();

        // Bind ExternalApiOptions from appsettings.json
        services.Configure<ExternalApiOptions>(
            configuration.GetSection(ExternalApiOptions.SectionName));

        return services;
    }
}
