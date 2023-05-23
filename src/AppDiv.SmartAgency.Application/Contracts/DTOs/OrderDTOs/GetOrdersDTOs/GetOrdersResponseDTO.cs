
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
public record GetOrdersResponseDTO
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string VisaNumber { get; set; } = string.Empty;
    public LookUpResponseDTO? Priority { get; set; }
    public OrderApplResponseDTO? Employee { get; set; }
    public OrderCriteriaResponseDTO? OrderCriteria { get; set; }
    public PaymentResponseDTO? Payment { get; set; }
    public SponsorResponseDTO? Sponsor { get; set; }
    public PartnerApplRespDTO? Partner { get; set; }
}