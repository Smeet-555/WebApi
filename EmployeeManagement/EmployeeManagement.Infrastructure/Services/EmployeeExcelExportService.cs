using ClosedXML.Excel;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;

namespace EmployeeManagement.Infrastructure.Services;

public class EmployeeExcelExportService : IExcelExportService
{
    public byte[] ExportEmployees(IEnumerable<EmployeeDto> employees)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Employees");

        // ── Header row ────────────────────────────────────────
        var headers = new[] { "ID", "First Name", "Last Name", "Email", "Department", "Active" };
        for (var col = 1; col <= headers.Length; col++)
        {
            var cell = sheet.Cell(1, col);
            cell.Value = headers[col - 1];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        // ── Data rows ─────────────────────────────────────────
        var row = 2;
        foreach (var emp in employees)
        {
            sheet.Cell(row, 1).Value = emp.Id;
            sheet.Cell(row, 2).Value = emp.FirstName;
            sheet.Cell(row, 3).Value = emp.LastName;
            sheet.Cell(row, 4).Value = emp.Email;
            sheet.Cell(row, 5).Value = emp.Department;
            sheet.Cell(row, 6).Value = emp.IsActive ? "Yes" : "No";

            // Alternate row shading
            if (row % 2 == 0)
            {
                sheet.Row(row).Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E1F2");
            }

            row++;
        }

        // ── Formatting ────────────────────────────────────────
        sheet.Columns().AdjustToContents();

        // Freeze header row
        sheet.SheetView.FreezeRows(1);

        // Auto-filter on header row
        sheet.RangeUsed()?.SetAutoFilter();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
