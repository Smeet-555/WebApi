using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Options;

// Bound from appsettings.json → "Smtp" section
public class SmtpOptions
{
    public const string SectionName = "Smtp";

    [Required]
    public string Host { get; set; } = string.Empty;

    [Required]
    public int Port { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FromEmail { get; set; } = string.Empty;

    public string FromName { get; set; } = "Employee Management";

    // Whether to use SSL/TLS
    public bool EnableSsl { get; set; } = true;
}
