

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly SmartAgencyDbContext _context;
    public OrderRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<Order>> GetAll()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task<ServiceResponse<Order>> GetOrderAsync(Guid id)
    {
        var response = new ServiceResponse<Order>();
        var order = new Order();
        try
        {
            order = await _context.Orders
                    .Include(order => order.OrderCriteria)
                    .Include(order => order.OrderSponsor)
                        .ThenInclude(os => os.SponsorAddress)
                    .Include(order => order.OrderPayment).FirstOrDefaultAsync(ord => ord.Id == id);
            if (order is not null)
            {
                response.Data = order;
                response.Success = true;
            }
            else
            {
                response.Message = new NotFoundException($"An order with id {id} is doesn't found.").Message;
                response.Success = false;
            }
        }
        catch (System.Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }
        return response;
    }

    public ServiceResponse<String> UpdateOrder(Order updatedOrder)
    {
        string message = null;
        try
        {
            await _context.Orders.Update(updatedOrder);
            message = "The order is updated.";
        }
        catch (System.Exception ex)
        {
            message = ex.Message;
        }
        return message;
    }

    public async Task<ServiceResponse<Int32>> SaveDbUpdateAsync()
    {
        var response = new ServiceResponse<Int32>();
        try
        {
            response.Data = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }
        return response;
    }
}