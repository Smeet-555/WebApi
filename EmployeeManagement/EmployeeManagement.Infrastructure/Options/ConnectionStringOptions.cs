using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Infrastructure.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required]
    public string DefaultConnection { get; set; } = string.Empty;
}
