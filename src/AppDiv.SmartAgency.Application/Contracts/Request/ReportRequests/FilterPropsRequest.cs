

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
public record FilterPropsRequest
{
    public string? PropertyName { get; set; }
    public string? MethodName { get; set; }
    public Object? Value { get; set; }
}