

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants. EditApplicantRequests;
public class EditBankAccountRequest
{
    public Guid Id { get; set; }
    public string? BankName { get; set; }
    public long AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }
}