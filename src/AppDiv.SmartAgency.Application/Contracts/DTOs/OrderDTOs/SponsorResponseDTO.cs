
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record SponsorResponseDTO
{
    public string? IdNumber { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAmharic { get; set; }
    public string? OtherName { get; set; }
    public string? ResidentialTitle { get; set; }
    public int NumberOfFamily { get; set; }
    public FileCollectionResponseDTO? SponsorIDFile { get; set; }
    public AddressResponseDTO? SponsorAddress { get; set; }
}