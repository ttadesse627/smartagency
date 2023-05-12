
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record DeletedOrderResponseDTO
{
    public int OrderNumber { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int ContractDuration { get; set; }
    public DateTime? VisaDate { get; set; }
    public string? ContractNumber { get; set; }
    public string? ElectronicVisaNumber { get; set; }
    public DateTime? ElectronicVisaDate { get; set; }
    public PartnerResponseDTO? Partner { get; set; }
    public LookUpResponseDTO? PortOfArrival { get; set; }
    public LookUpResponseDTO? Priority { get; set; }
    public LookUpResponseDTO? VisaType { get; set; }
    public OrderApplResponseDTO? Employee { get; set; }
    public FileCollectionResponseDTO? VisaFile { get; set; }
    public OrderCriteriaResponseDTO? OrderCriteria { get; set; }
    public PaymentResponseDTO? OrderPayment { get; set; }
    public SponsorResponseDTO? OrderSponsor { get; set; }
}