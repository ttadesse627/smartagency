namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
public record PersonalInfoResponseDTO
{
    public Guid Id { get; set; }
    public string? Nationality { get; set; }
    public string? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? MaritalStatus { get; set; }
    public int NumberOfChildren { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string? EducationQualification { get; set; }
    public string? PhoneNumber { get; set; }

}