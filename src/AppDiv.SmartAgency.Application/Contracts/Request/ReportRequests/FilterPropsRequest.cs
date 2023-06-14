

using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
public record FilterPropsRequest
{
    [JsonProperty("propertyName")]
    public string? PropertyName { get; set; }

    [JsonProperty("methodName")]
    public string? MethodName { get; set; }

    [JsonProperty("value")]
    public object? Value { get; set; }
}