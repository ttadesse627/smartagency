

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record GetOrderNumberResponseDTO
{

    public ICollection<GetApplForAssignmentDTO>? UnAssignedApplicants { get; set; }
    public ICollection<GetPartnerDropDownDTO>? partners { get; set; }

}