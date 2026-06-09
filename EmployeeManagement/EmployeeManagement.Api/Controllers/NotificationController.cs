using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NotificationController : ControllerBase
{
    private readonly IEmailService _emailService;

    public NotificationController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    /// <summary>Send a test email.</summary>
    /// <response code="200">Email sent successfully.</response>
    /// <response code="500">Failed to send email.</response>
    [HttpPost("send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Send([FromBody] SendEmailRequest request)
    {
        await _emailService.SendAsync(request.ToEmail, request.Subject, request.Body);
        return Ok(new { Message = $"Email sent to {request.ToEmail}" });
    }
}

// Simple request model — no need for a separate DTO file
public class SendEmailRequest
{
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
