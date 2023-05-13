

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.DeletedInfoDTOs;
public record DeletedOrderCriteriaResponseDTO
{
    public Guid Id { get; set; }
    public LookUpResponseDTO? OrderCriteriaJobTitle { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
}