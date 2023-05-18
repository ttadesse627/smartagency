

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class BankAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? BankName { get; set; }
    public long AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }

    public Guid? ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
}