

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class BankAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? BankName { get; set; }
    public long AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }

    // Navigation properties
    public Applicant? Applicant { get; set; }
}