using System.Text.Json.Serialization;

namespace AppDiv.SmartAgency.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PermissionEnum
{
    AccessMember,
    WriteMember,
    ReadMember,
    ReadDetailMember,
    UpdateMember,
    DeleteMember,
}

