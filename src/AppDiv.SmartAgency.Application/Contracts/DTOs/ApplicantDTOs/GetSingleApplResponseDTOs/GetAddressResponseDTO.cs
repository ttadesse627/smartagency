
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
public record GetAddressResponseDTO
{
    public Guid Id { get; set; }
    public string? SubCity { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? Kebele { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? OfficePhone { get; set; }
    public string? Mobile { get; set; }
    public string? AlternativePhone { get; set; }
    public string? Fax { get; set; }
    public required string Adress { get; set; }
    public string? PostCode { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public LookUpResponseDTO? Region { get; set; }
    //  public LookUpResponseDTO? Country { get; set; }
}