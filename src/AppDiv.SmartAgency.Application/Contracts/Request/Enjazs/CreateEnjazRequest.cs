

namespace AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
public record CreateEnjazRequest
{
    public ICollection<AddEnjazRequest>? Enjazs { get; set; }
}