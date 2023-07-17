
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.EnjazDTOs
{
    public class EnjazResponseDTO
    {
        public string? EnjazNumber { get; set; }
        public string? OrderNumber { get; set; }
        public string? VisaNumber { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? PassportNumber { get; set; }
        public string? EmployeeName { get; set; }
        public string? SponsorName { get; set; }
    }
}