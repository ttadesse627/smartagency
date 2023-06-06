

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetProcessResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Step { get; set; }
    public LookUpResponseDTO? Country { get; set; }
    public ICollection<GetProcessDefinitionResponseDTO>? ProcessDefinitions { get; set; }
}