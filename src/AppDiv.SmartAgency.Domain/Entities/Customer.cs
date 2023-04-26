using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities;
// Customer entity 
public class Customer : BaseAuditableEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public Gender Gender { get; set; } = Gender.Male;
    public string? SuffixId { get; set; }
    public Suffix? Suffix { get; set; }
}
