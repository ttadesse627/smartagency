

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public record BankAccountRequest
{
    public string? BankName { get; set; }
    public int AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }
}