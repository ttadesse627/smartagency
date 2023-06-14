

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
public record ApplReportDTO
{
    public SearchModel<ApplicantReportResponseDTO> Applicants { get; set; }
    public ICollection<string> FilterProperties { get; set; }
}