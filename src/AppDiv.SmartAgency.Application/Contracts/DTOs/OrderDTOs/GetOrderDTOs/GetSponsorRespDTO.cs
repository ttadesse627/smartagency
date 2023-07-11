
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
public class GetSponsorRespDTO
{
    public string? IdNumber { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAmharic { get; set; }
    public string? FullNameArabic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }
    public AttachmentFileResponseDTO? Attachment { get; set; }
    public GetSponsorAddressRespDTO? Address { get; set; }
}