using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Common;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Errors;
using EmployeeManagement.Domain.Repositories;

namespace EmployeeManagement.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<EmployeeDto>>> GetAllAsync()
    {
        var employees = await _repository.GetAllAsync();
        var dtos = employees.Select(MapToDto);
        return Result<IEnumerable<EmployeeDto>>.Success(dtos);
    }

    public async Task<Result<EmployeeDto>> GetByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee is null)
            return Result<EmployeeDto>.Failure(EmployeeErrors.NotFound);

        return Result<EmployeeDto>.Success(MapToDto(employee));
    }

    public async Task<Result<EmployeeDto>> CreateAsync(CreateEmployeeDto dto)
    {
        // Check if email is already taken
        var emailExists = await _repository.EmailExistsAsync(dto.Email);
        if (emailExists)
            return Result<EmployeeDto>.Failure(EmployeeErrors.EmailAlreadyExists);

        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Department = dto.Department
        };

        await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();

        return Result<EmployeeDto>.Success(MapToDto(employee));
    }

    public async Task<Result> UpdateAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee is null)
            return Result.Failure(EmployeeErrors.NotFound);

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Department = dto.Department;
        employee.IsActive = dto.IsActive;

        await _repository.UpdateAsync(employee);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee is null)
            return Result.Failure(EmployeeErrors.NotFound);

        await _repository.DeleteAsync(employee);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }

    // Simple manual mapping — no AutoMapper needed
    private static EmployeeDto MapToDto(Employee employee) => new()
    {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        Department = employee.Department,
        IsActive = employee.IsActive
    };
}
