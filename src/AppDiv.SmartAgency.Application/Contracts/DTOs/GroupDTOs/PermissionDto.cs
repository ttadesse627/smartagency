using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
public class PermissionDto
{
    public Guid Id { get; set; }
    public ResourceResponseDTO Resource { get; set; } = null!;
    public List<string> Actions { get; set; } = [];
}