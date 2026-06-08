namespace EmployeeManagement.Domain.Common;

// Non-generic Result — used when there is no return value (e.g. delete, update)
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    private Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    // Factory methods
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}
