namespace AppDiv.SmartAgency.Application.Contracts.DTOs.RoleDTOs;
public class RoleDto
{
    public string Page { get; set; }
    public string Title { get; set; }
    public bool CanAdd { get; set; } = true;
    public bool CanDelete { get; set; } = true;
    public bool CanViewDetail { get; set; } = true;
    public bool CanView { get; set; } = true;
    public bool CanUpdate { get; set; } = true;
}