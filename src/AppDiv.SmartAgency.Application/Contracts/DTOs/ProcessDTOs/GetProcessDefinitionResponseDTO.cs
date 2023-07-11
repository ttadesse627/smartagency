

using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetProcessDefinitionResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Step { get; set; }
    public ICollection<Guid>? NextPdIds { get; set; }
    public ICollection<GetApplProcessResponseDTO>? ApplicantProcesses { get; set; }

}