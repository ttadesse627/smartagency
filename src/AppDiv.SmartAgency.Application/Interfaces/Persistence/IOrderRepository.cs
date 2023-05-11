
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<List<Order>> GetAll();
    public Task<ServiceResponse<Order>> GetOrderAsync(Guid id);
    public Task<ServiceResponse<Int32>> SaveDbUpdateAsync();
    public string UpdateOrder(Order updatedOrder);
}