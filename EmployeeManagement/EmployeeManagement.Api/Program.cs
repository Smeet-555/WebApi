using EmployeeManagement.Api.Extensions;
using EmployeeManagement.Api.Middleware;
using EmployeeManagement.Application;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Scheduler;
using EmployeeManagement.Scheduler.Jobs;
using Hangfire;
using Hangfire.InMemory;
using Serilog;

// ── Bootstrap Serilog ──────────────────────────────────────
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .Build())
    .CreateLogger();

try
{
    Log.Information("Starting EmployeeManagement API...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // ── Register services ──────────────────────────────────
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocs();

    // Feature layers
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddScheduler();

    // ── Hangfire — in-memory storage (no DB required) ──────
    builder.Services.AddHangfire(config =>
        config.UseInMemoryStorage());

    builder.Services.AddHangfireServer();

    // ── Build app ──────────────────────────────────────────
    var app = builder.Build();

    // ── Middleware pipeline ────────────────────────────────
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseSerilogRequestLogging();

    // Swagger UI at /swagger
    app.UseSwaggerDocs();

    // Hangfire Dashboard at /hangfire
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        // Allow access without auth in development
        Authorization = []
    });

    // ── Register recurring job ─────────────────────────────
    // Runs EmployeeSyncJob every 60 minutes
    RecurringJob.AddOrUpdate<EmployeeSyncJob>(
        recurringJobId: "employee-sync",
        methodCall: job => job.ExecuteAsync(),
        cronExpression: "0 * * * *"   // every hour
    );

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}
