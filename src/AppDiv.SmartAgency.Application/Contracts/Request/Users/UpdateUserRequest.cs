

namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
public record UpdateUserRequest
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public Guid? PositionId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? PartnerId { get; set; }
    public virtual UpdateAddressRequest? Address { get; set; }
    public virtual ICollection<Guid>? UserGroups { get; set; }
}