using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Application.Interfaces;

public interface IEmployeeService
{
    Task<Result<IEnumerable<EmployeeDto>>> GetAllAsync();
    Task<Result<EmployeeDto>> GetByIdAsync(int id);
    Task<Result<EmployeeDto>> CreateAsync(CreateEmployeeDto dto);
    Task<Result> UpdateAsync(int id, UpdateEmployeeDto dto);
    Task<Result> DeleteAsync(int id);
}
