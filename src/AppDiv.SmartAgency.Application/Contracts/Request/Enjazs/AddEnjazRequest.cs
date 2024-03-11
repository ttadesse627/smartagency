

namespace AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
public record AddEnjazRequest
{
    public string? ApplicationNumber { get; set; }
    public string? TransactionCode { get; set; }
    public Guid ApplicantId { get; set; }
}