

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record ApplicantProcessResponseDTO
{
    public ICollection<GetProcessDefinitionResponseDTO>? ProcessDefinitions { get; set; }
    public ICollection<GetApplProcessResponseDTO>? NotStartedApplicants { get; set; }

}