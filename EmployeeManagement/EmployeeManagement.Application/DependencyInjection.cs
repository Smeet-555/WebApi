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

        // Bind and validate ExternalApiOptions at startup
        services.AddOptions<ExternalApiOptions>()
            .Bind(configuration.GetSection(ExternalApiOptions.SectionName))
            .ValidateDataAnnotations()        // fails fast if BaseUrl or ApiKey is missing
            .ValidateOnStart();

        return services;
    }
}
