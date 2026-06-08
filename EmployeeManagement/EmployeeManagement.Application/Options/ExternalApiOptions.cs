using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Options;

// Bound from appsettings.json → "ExternalApi" section
public class ExternalApiOptions
{
    public const string SectionName = "ExternalApi";

    [Required]
    public string BaseUrl { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}
