
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs
{
    public class LookUpResponseDTO
    {
    public string Id { get; set; }
    
    public Category Category { get; set; }

    public string Value { get; set; } = string.Empty;
    //public bool IsRequired { get; set; }
    //public bool ShowOnCv { get; set; }

    }
}