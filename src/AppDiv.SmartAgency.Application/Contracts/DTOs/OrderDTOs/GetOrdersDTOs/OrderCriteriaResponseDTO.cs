

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
public record OrderCriteriaResponseDTO
{
    public LookUpResponseDTO? JobTitle { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
}