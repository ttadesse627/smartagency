

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public record GetWitnessResponseDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}