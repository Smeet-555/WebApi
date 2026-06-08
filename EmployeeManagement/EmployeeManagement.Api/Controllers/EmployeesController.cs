using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>Get all employees.</summary>
    /// <response code="200">Returns the list of employees.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _employeeService.GetAllAsync();
        return Ok(result.Value);
    }

    /// <summary>Get a single employee by ID.</summary>
    /// <response code="200">Returns the employee.</response>
    /// <response code="404">Employee not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _employeeService.GetByIdAsync(id);

        if (result.IsFailure)
            return NotFound(result.Error.Message);

        return Ok(result.Value);
    }

    /// <summary>Create a new employee.</summary>
    /// <response code="201">Employee created successfully.</response>
    /// <response code="400">Email already exists or invalid data.</response>
    [HttpPost]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var result = await _employeeService.CreateAsync(dto);

        if (result.IsFailure)
            return BadRequest(result.Error.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    /// <summary>Update an existing employee.</summary>
    /// <response code="204">Employee updated successfully.</response>
    /// <response code="404">Employee not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var result = await _employeeService.UpdateAsync(id, dto);

        if (result.IsFailure)
            return NotFound(result.Error.Message);

        return NoContent();
    }

    /// <summary>Delete an employee.</summary>
    /// <response code="204">Employee deleted successfully.</response>
    /// <response code="404">Employee not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _employeeService.DeleteAsync(id);

        if (result.IsFailure)
            return NotFound(result.Error.Message);

        return NoContent();
    }
}
