


namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record EditProcessRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Step { get; set; }
    public bool IsVisaRequired { get; set; }
    public Guid? CountryId { get; set; }
    public ICollection<EditProcessDefinitionRequest>? ProcessDefinitions { get; set; }
}