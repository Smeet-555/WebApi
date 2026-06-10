using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Options;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.ExternalApis;
using EmployeeManagement.Infrastructure.Logging;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Infrastructure.Services;using Microsoft.EntityFrameworkCore;
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
        services.AddOptions<SmtpOptions>()
            .Bind(configuration.GetSection(SmtpOptions.SectionName))
            .ValidateDataAnnotations();
            // Note: not using ValidateOnStart so missing SMTP config won't block startup

        // ── Database — In-Memory (swap to UseSqlServer for production) ───
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("EmployeeManagementDb"));

        // ── Repositories ──────────────────────────────────
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        // ── External APIs ─────────────────────────────────
        services.AddHttpClient<IExternalUserService, ExternalUserService>();

        // ── Email ─────────────────────────────────────────
        services.AddScoped<IEmailService, EmailService>();

        // ── Excel Export ──────────────────────────────────────
        services.AddScoped<IExcelExportService, EmployeeExcelExportService>();

        return services;
    }
}
