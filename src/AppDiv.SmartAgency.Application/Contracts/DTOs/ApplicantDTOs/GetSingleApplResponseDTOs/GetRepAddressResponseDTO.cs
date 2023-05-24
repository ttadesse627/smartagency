
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
public record GetRepAddressResponseDTO
{
    public Guid Id { get; set; }
    public string? City { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string? Kebele { get; set; }
    public string? PhoneNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string Adress { get; set; }

}