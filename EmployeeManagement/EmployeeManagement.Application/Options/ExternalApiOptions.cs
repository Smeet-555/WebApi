namespace EmployeeManagement.Application.Options;

// Bound from appsettings.json section "ExternalApi"
public class ExternalApiOptions
{
    public const string SectionName = "ExternalApi";

    public string BaseUrl { get; set; } = string.Empty;

    // Optional — not all external APIs require a key
    public string ApiKey { get; set; } = string.Empty;
}
