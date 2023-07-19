

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class NewAssignedVisaResponseDTO
    {
        public ICollection<string> Columns { get; set; } = new string[] { "hh", "hh" };
        public string VisaNumber { get; set; }
        public DateTime Date { get; set; }
        public string JobTitle { get; set; }
        public string Salary { get; set; }
        public Guid ApplicantId { get; set; }
        public string Employee { get; set; }
        public string Country { get; set; }

    }
}