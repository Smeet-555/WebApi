using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Interfaces;

public interface IExcelExportService
{
    byte[] ExportEmployees(IEnumerable<EmployeeDto> employees);
}
