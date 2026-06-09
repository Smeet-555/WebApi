using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Scheduler.Jobs;
using Hangfire;

namespace EmployeeManagement.Scheduler;


public class HangfireJobEnqueuer : IJobEnqueuer
{
    private readonly IBackgroundJobClient _jobClient;

    public HangfireJobEnqueuer(IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
    }

    public void EnqueueWelcomeEmail(int employeeId)
    {
        _jobClient.Enqueue<WelcomeEmailJob>(job => job.ExecuteAsync(employeeId));
    }
}
