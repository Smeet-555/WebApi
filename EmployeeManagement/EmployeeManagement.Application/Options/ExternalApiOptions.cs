using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Options;

public class ExternalApiOptions
{
    public const string SectionName = "ExternalApi";

    [Required]
    public string BaseUrl { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;
}
