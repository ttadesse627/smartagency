
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IOrderRepository : IBaseRepository<Order>
{
    // public Task<List<Order>> GetAll();
    public Task<ServiceResponse<Order>> GetOrderAsync(Guid id);
    public Task<ServiceResponse<Int32>> SaveDbUpdateAsync();
    public ServiceResponse<String> UpdateOrder(Order updatedOrder);

    public Task<List<NotAssignedVisaResponseDTO>> GetNotAssignedVisa();
    public Task<List<VisaExpiryResponseDTO>> GetExpiredVisa();
    public Task<List<PenalityResponseDTO>> GetPenality();
    public Task<List<ComplaintResponseDTO>> GetComplaints();
    // public Task<List<GetUnAssignedOrdersDropdownResponseDTO>> GetUnAssignedOrdersDropDown();
    public Task<GetUnAssignedOrdersResponseDTO> GetUnAssignedOrdersDropDown();
}