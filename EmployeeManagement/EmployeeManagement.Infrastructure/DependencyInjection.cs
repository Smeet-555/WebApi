using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.ExternalApis;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register EF Core with SQL Server
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register repository
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        // Register external user service with a named HttpClient
        services.AddHttpClient<IExternalUserService, ExternalUserService>();

        return services;
    }
}
