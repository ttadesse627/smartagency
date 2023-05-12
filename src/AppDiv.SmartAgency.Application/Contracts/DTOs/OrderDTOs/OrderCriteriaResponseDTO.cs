

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record OrderCriteriaResponseDTO
{
    public Guid Id { get; set; }
    public int? Age { get; set; }
    public string? Remark { get; set; }
    public LookUpResponseDTO? Nationality { get; set; }
    public LookUpResponseDTO? OrderCriteriaJobTitle { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? Experience { get; set; }
    public LookUpResponseDTO? Language { get; set; }
}