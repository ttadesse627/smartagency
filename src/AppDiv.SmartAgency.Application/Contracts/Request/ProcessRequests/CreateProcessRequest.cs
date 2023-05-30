

using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record CreateProcessRequest
{
    public string? Name { get; set; }
    public int Step { get; set; }
    public bool IsVisaRequired { get; set; }
    public Guid? CountryId { get; set; } = null;
    public ICollection<CreateProcessDefinitionRequest>? ProcessDefinitions { get; set; }
}