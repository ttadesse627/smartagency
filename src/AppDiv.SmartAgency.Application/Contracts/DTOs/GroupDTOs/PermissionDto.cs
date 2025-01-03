using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
public class PermissionDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<string> Actions { get; set; } = [];
}