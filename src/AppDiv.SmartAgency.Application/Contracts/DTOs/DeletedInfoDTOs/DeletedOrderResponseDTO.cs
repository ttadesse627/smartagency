
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.DeletedInfoDTOs;
public record DeletedOrderResponseDTO
{
    public string? OrderNumber { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? VisaDate { get; set; }
    public string? VisaNumber { get; set; }
    public DeletedOrderCriteriaResponseDTO? OrderCriteria { get; set; }
    public DeletedOrderApplResponseDTO? Employee { get; set; }
}