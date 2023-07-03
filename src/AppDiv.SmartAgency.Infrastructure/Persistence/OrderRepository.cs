

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Exceptions;
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
                    .Include("PortOfArrival")
                    .Include("Priority")
                    .Include("VisaType")
                    .Include("PortOfArrival")
                    .Include("AttachmentFile")
                    .Include("OrderCriteria")
                    .Include("OrderCriteria.Nationality")
                    .Include("OrderCriteria.JobTitle")
                    .Include("OrderCriteria.Salary")
                    .Include("OrderCriteria.Religion")
                    .Include("OrderCriteria.Experience")
                    .Include("OrderCriteria.Language")
                    .Include("Sponsor")
                    .Include("Sponsor.AttachmentFile")
                    .Include("Sponsor.Address")
                    .Include("Sponsor.Address.Region")
                    .Include("Sponsor.Address.Country")
                    .Include("Sponsor.Address.City")
                    .Include("Payment")
                    .Include("Employees")
                    .Include("Partner").FirstOrDefaultAsync(ord => ord.Id == id);
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
        var response = new ServiceResponse<String>();
        try
        {
            _context.Orders.Update(updatedOrder);
            response.Data = "Successfully set";
            response.Message = "The order is updated.";
            response.Success = true;
            response.Errors?.Add("No error found!");
        }
        catch (System.Exception ex)
        {
            response.Data = null;
            response.Message = "Cannot update the order because of some error/s";
            response.Success = false;
            response.Errors?.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<Int32>> SaveDbUpdateAsync()
    {
        var response = new ServiceResponse<Int32>();
        try
        {
            response.Data = await _context.SaveChangesAsync();
            response.Message = "The update saved successfully";
        }
        catch (Exception ex)
        {
            response.Data = 0;
            response.Message = ex.Message;
            response.Success = false;
            response.Errors?.Add(ex.Message);
        }
        return response;
    }


    public async Task<List<NotAssignedVisaResponseDTO>> GetNotAssignedVisa()
    {
          var response= await _context.Orders
                             .Where( or=> or.Employees== null)
                             .Select( or=> new NotAssignedVisaResponseDTO{
                                     AgencyName = or.Partner.PartnerName,
                                     OrderNumber = or.OrderNumber,
                                     VisaNumber = or.VisaNumber,
                                     Duration = DateTime.Now.Subtract(or.CreatedAt).Days,
                                     Job = or.OrderCriteria.JobTitle.Value,
                                     Sponsor = or.Sponsor.FullName,
                                     Age = (int)or.OrderCriteria.Age,
                                     Language = or.OrderCriteria.Language.Value,
                                     Expereince = or.OrderCriteria.Experience.Value,
                                     NoOfVisa= or.NumberOfVisa,
                                     Religion = or.OrderCriteria.Religion.Value

                             } ).ToListAsync();

                return response;          

    }

    public async Task<List<VisaExpiryResponseDTO>> GetExpiredVisa()
    {
        // var orders = await _context.Orders.
        var response = await _context.Applicants
                        .Where(app => app.OrderId != null && DateTime.Now.Subtract(app.Order.CreatedAt).Days >  _context.CountryOperations.Where(
                        co => co.CountryId == app.Order.Sponsor.Address.CountryId).FirstOrDefault().VisaExpiryDays
                        ).Select(
                            orr=> new VisaExpiryResponseDTO{
                                EmployerName= orr.Order.Sponsor.FullName,
                                EmployerPhoneNumber = orr.Order.Sponsor.Address.PhoneNumber,
                                EmployeeName= orr.FirstName + " " +orr.MiddleName + " " +orr.LastName,
                                 PassportNumber = orr.PassportNumber,
                                  WorkingCountry = orr.Order.Sponsor.Address.Country.Value,
                                  Sex = orr.Gender.ToString()
                            } 
                        ).ToListAsync();
       return response;


    }
}