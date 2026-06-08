namespace EmployeeManagement.Application.DTOs;

// Sent by the client when updating an existing employee
public class UpdateEmployeeDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
