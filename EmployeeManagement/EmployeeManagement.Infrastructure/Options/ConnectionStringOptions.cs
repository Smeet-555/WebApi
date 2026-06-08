using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Infrastructure.Options;

// Bound from appsettings.json → "ConnectionStrings" section
public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required]
    public string DefaultConnection { get; set; } = string.Empty;
}
