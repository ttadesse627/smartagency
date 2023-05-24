

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
public class GetOrderCriteriaRespDTO
{
    public LookUpResponseDTO? Nationality { get; set; }
    public LookUpResponseDTO? JobTitle { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? Experience { get; set; }
    public LookUpResponseDTO? Language { get; set; }
    public int? Age { get; set; }
    public string? Remark { get; set; }
}