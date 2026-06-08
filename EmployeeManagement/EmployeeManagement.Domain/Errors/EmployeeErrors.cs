using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Domain.Errors;

// All employee-related errors defined in one place
public static class EmployeeErrors
{
    public static readonly Error NotFound =
        Error.Create("Employee.NotFound", "Employee with the given ID was not found.");

    public static readonly Error EmailAlreadyExists =
        Error.Create("Employee.EmailAlreadyExists", "An employee with this email already exists.");

    public static readonly Error InvalidData =
        Error.Create("Employee.InvalidData", "The provided employee data is invalid.");
}
