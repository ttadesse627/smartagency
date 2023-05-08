namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record BankAccountResponseDTO
{
    public Guid Id { get; set; }
    public string? BankName { get; set; }
    public long AccountNumber { get; set; }
    public string? BranchName { get; set; }
    public string? SwiftCode { get; set; }
}