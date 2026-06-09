using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Options;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.ExternalApis;
using EmployeeManagement.Infrastructure.Logging;
using EmployeeManagement.Infrastructure.Options;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ── Logging ───────────────────────────────────────
        services.AddInfrastructureLogging();

        // ── Options ───────────────────────────────────────
        services.AddOptions<ConnectionStringOptions>()
            .Bind(configuration.GetSection(ConnectionStringOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<SmtpOptions>()
            .Bind(configuration.GetSection(SmtpOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // ── Database ──────────────────────────────────────
        var connectionString = configuration
            .GetSection(ConnectionStringOptions.SectionName)
            .Get<ConnectionStringOptions>()!
            .DefaultConnection;

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // ── Repositories ──────────────────────────────────
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        // ── External APIs ─────────────────────────────────
        services.AddHttpClient<IExternalUserService, ExternalUserService>();

        // ── Email ─────────────────────────────────────────
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
