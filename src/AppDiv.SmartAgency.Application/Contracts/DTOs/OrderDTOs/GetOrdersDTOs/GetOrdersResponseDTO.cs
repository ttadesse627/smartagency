
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
public record GetOrdersResponseDTO
{
    // public Guid Id { get; set; }
    // public DateTime CreatedAt { get; set; }
    // public string OrderNumber { get; set; }
    // public string VisaNumber { get; set; }
    // public LookUpResponseDTO? Priority { get; set; }
    // public OrderApplResponseDTO? Employee { get; set; }
    // public OrderCriteriaResponseDTO? OrderCriteria { get; set; }
    // public PaymentResponseDTO? Payment { get; set; }
    // public SponsorResponseDTO? Sponsor { get; set; }
    // public PartnerApplRespDTO? Partner { get; set; }

    //
    public Guid Id { get; set; }
    public DateTime RegisteredDate { get; set; }
    public string? OrderNumber { get; set; }
    public string? VisaNumber { get; set; }
    public string? Priority { get; set; }
    public Guid? ApplicantId { get; set; }
    public string? PassportNumber { get; set; }
    public string? EmployeeName { get; set; }
    public string? JobTitle { get; set; }
    public string? Salary { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public string? SponsorIdNumber { get; set; }
    public string? SponsorFullName { get; set; }
    public string? PartnerName { get; set; }

}