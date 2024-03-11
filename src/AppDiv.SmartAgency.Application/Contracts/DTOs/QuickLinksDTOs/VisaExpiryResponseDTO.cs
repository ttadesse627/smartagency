namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class VisaExpiryResponseDTO
    {
        public string? EmployerName { get; set; }
        public string? EmployerPhoneNumber { get; set; }
        public string? EmployeeName { get; set; }
        public string? PassportNumber { get; set; }
        public string? WorkingCountry { get; set; }
        public string? Sex { get; set; }
        public DateTime VisaExpired { get; set; }
        public int DatePassed { get; set; }

    }
}