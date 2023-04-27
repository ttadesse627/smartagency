

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class BankAccount
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? BankName { get; set; }
    public int AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }
    public Applicant Applicant { get; set; }
}