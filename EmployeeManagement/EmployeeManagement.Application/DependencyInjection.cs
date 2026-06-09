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
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddOptions<ExternalApiOptions>()
            .Bind(configuration.GetSection(ExternalApiOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
