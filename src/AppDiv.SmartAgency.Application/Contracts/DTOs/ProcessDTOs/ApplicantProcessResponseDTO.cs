

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record ApplicantProcessResponseDTO
{
    public ICollection<GetApplProcessResponseDTO>? ProcessReadyApplicants { get; set; }
    public ICollection<GetProcessDefinitionResponseDTO>? ProcessDefinitions { get; set; }

}