using System.Text.Json.Serialization;

namespace AppDiv.SmartAgency.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PermissionEnum 
{
    Access,
    Write,
    Read,
    ReadDetail,
    Update,
    Delete
}

