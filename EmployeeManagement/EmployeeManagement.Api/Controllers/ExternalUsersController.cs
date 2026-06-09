using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExternalUsersController : ControllerBase
{
    private readonly IExternalUserService _externalUserService;

    public ExternalUsersController(IExternalUserService externalUserService)
    {
        _externalUserService = externalUserService;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExternalUserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _externalUserService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExternalUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _externalUserService.GetByIdAsync(id);

        if (user is null)
            return NotFound(new { Message = $"User with Id {id} not found in external API." });

        return Ok(user);
    }


    [HttpGet("exists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UserExists([FromQuery] string email)
    {
        var exists = await _externalUserService.UserExistsAsync(email);
        return Ok(new { Email = email, Exists = exists });
    }

    [HttpPost("sync")]
    [ProducesResponseType(typeof(IEnumerable<ExternalUserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Sync()
    {
        var users = await _externalUserService.GetAllAsync();
        await _externalUserService.SyncUsersAsync();
        return Ok(users);
    }
}
