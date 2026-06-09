namespace EmployeeManagement.Application.Interfaces;

// Abstraction over background job scheduling.
// Keeps the Application layer free of Hangfire dependencies.
public interface IJobEnqueuer
{
    // Enqueues a fire-and-forget welcome email job for the given employee.
    void EnqueueWelcomeEmail(int employeeId);
}
