namespace EmployeeManagement.Application.Options;

// Bound from appsettings.json section "ExternalApi"
public class ExternalApiOptions
{
    public const string SectionName = "ExternalApi";

    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}
