

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
public record GetSponsorAddressRespDTO
{
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? OfficePhone { get; set; }
    public string? Mobile { get; set; }
    public string? AlternativePhone { get; set; }
    public string? Fax { get; set; }
    public string Adress { get; set; } = string.Empty;
    public string? PostCode { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public LookUpResponseDTO? Country { get; set; }
    public LookUpResponseDTO? Region { get; set; }
    public LookUpResponseDTO? City { get; set; }
}