
using System.Text.Json.Serialization;

namespace AppDiv.SmartAgency.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MaritalStatus
{
    Single,
    Married,
    Divorced
}