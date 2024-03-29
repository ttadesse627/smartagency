


using AppDiv.SmartAgency.Application.Contracts.Request.Common;

namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
public class AddUserRequest
{
    public required string UserName { get; set; }
    public string? FullName { get; set; }
    public Guid? PositionId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? PartnerId { get; set; }
    public virtual AddressRequest? Address { get; set; }
    public virtual ICollection<Guid>? UserGroups { get; set; }
    public required string Password { get; set; }
    public required string ConfirmationPassword { get; set; }
}