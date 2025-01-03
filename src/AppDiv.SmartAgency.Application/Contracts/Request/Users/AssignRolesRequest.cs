namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
public class AssignRolesRequest
{
    public Guid UserId { get; set; }
    public virtual ICollection<Guid> GroupIds { get; set; } = [];
}