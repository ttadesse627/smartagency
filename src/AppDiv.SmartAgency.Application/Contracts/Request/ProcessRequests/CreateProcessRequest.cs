

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record CreateProcessRequest
{
    public string? Name { get; set; }
    public int Step { get; set; }
    public bool IsVisaRequired { get; set; }
    public bool EnjazRequired { get; set; }
    public Guid? CountryId { get; set; }
    public ICollection<CreateProcessDefinitionRequest>? ProcessDefinitions { get; set; }
}