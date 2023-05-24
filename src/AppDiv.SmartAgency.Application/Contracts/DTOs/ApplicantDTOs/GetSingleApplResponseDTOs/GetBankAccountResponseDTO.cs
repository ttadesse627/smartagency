

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public class GetBankAccountResponseDTO
{
    public Guid Id { get; set; }
    public string? BankName { get; set; }
    public long AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }
}