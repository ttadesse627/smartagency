

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
public class BankAccountRequest
{
    public string? BankName { get; set; }
    public long? AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }
}