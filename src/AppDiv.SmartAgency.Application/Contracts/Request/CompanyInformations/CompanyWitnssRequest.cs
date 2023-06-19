

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
public record CompanyWitnssRequest
{
    public ICollection<CompanyWitnessRequest>? Witnesses { get; set; }
}