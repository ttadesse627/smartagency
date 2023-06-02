

namespace AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
public record AddEnjazRequest
{
    public string ApplicationNumber { get; set; }
    public int TransactionCode { get; set; }
    public Guid? SponsorId { get; set; }
}