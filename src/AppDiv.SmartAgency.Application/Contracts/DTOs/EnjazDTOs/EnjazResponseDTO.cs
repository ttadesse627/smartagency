
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.EnjazDTOs
{
    public class EnjazResponseDTO
    {
        public string? EnjazNumber { get; set; }
        public string? OrderNumber { get; set; }
        public string? VisaNumber { get; set; }
        public Guid? OrderId { get; set; }
        public string? PassportNumber { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
    }
}