
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
public record GetOrderRespDTO
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string VisaNumber { get; set; } = string.Empty;
    public int ContractDuration { get; set; }
    public DateTime? VisaDate { get; set; }
    public DateTime OrderDate { get; set; }
    public string? ContractNumber { get; set; }
    public string? ElectronicVisaNumber { get; set; }
    public DateTime? ElectronicVisaDate { get; set; }
    public LookUpResponseDTO? PortOfArrival { get; set; }
    public LookUpResponseDTO? Priority { get; set; }
    public LookUpResponseDTO? VisaType { get; set; }
    public OrderApplResponseDTO? Employee { get; set; }
    public AttachmentFileResponseDTO? AttachmentFile { get; set; }
    public GetOrderCriteriaRespDTO? OrderCriteria { get; set; }
    public GetPaymentRespDTO? Payment { get; set; }
    public GetSponsorRespDTO? Sponsor { get; set; }
    public PartnerApplRespDTO? Partner { get; set; }
}