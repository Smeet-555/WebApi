using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Scheduler.Jobs;

public class WelcomeEmailJob
{
    private readonly IEmployeeRepository _repository;
    private readonly IEmailService _emailService;
    private readonly ILogger<WelcomeEmailJob> _logger;

    public WelcomeEmailJob(
        IEmployeeRepository repository,
        IEmailService emailService,
        ILogger<WelcomeEmailJob> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task ExecuteAsync(int employeeId)
    {
        _logger.LogInformation("WelcomeEmailJob started for EmployeeId={EmployeeId}", employeeId);

        var employee = await _repository.GetByIdAsync(employeeId);

        if (employee is null)
        {
            _logger.LogWarning("WelcomeEmailJob: Employee {EmployeeId} not found. Skipping.", employeeId);
            return;
        }

        var subject = "Welcome to Employee Management!";
        var body = $"""
            <h2>Welcome, {employee.FirstName}!</h2>
            <p>Your account has been successfully created.</p>
            <ul>
                <li><strong>Name:</strong> {employee.FirstName} {employee.LastName}</li>
                <li><strong>Email:</strong> {employee.Email}</li>
                <li><strong>Department:</strong> {employee.Department}</li>
            </ul>
            <p>We're glad to have you on board.</p>
            """;

        await _emailService.SendAsync(employee.Email, subject, body);

        _logger.LogInformation(
            "WelcomeEmailJob completed for EmployeeId={EmployeeId} Email={Email}",
            employeeId, employee.Email);
    }
}
