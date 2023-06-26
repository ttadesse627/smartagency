
namespace AppDiv.SmartAgency.Domain.SqlViews;
public class ApplicantInfo
{
    public string PassportNumber { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string PlaceOfBirth { get; set; }
    public int NumberOfChildren { get; set; }
    public string? MotherName { get; set; }
    public string? PreviousNationality { get; set; }
    public string? CurrentNationality { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? ModifiedAt { get; set; }
}