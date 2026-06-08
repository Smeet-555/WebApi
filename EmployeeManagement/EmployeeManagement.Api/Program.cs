using EmployeeManagement.Api.Extensions;
using EmployeeManagement.Api.Middleware;
using EmployeeManagement.Application;
using EmployeeManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ── Register services ──────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocs();             // custom Swagger config

// Application and Infrastructure layers
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

// ── Build app ──────────────────────────────────────────────
var app = builder.Build();

// ── Middleware pipeline ────────────────────────────────────
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();                      // Swagger UI at https://localhost/
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
