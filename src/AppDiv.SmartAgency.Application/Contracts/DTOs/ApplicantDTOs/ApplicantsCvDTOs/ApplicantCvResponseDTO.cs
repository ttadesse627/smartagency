using System.Net.Mail;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record ApplicantCvResponseDTO
{
    public OverviewResponseDTO? Overview { get; set; }
    public PersonalInfoResponseDTO? PersonalInfo { get; set; }
    public ICollection<OverseasExperienceResponseDTO>? OverseasExperiences { get; set; }
    public ICollection<String>? Skills { get; set; }
    public ICollection<LanguagesResponseDTO>? Languages { get; set; }
    public PassportInfoResponseDTO? PassportInfo { get; set; }
    public AttachmentsResponseDTO? Attachments { get; set; }
    public ICollection<String>? AttachmentNames { get; set; }
}