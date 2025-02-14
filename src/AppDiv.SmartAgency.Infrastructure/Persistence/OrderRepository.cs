using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly SmartAgencyDbContext _context;
    public OrderRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
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

    public async Task<ServiceResponse<int>> SaveDbUpdateAsync()
    {
        var response = new ServiceResponse<int>();
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
        var response = await _context.Orders
                           .Where(or => (or.IsDeleted == false) && (or.Employees == null || or.Employees.Count() == 0))
                           .Select(or => new NotAssignedVisaResponseDTO
                           {
                               AgencyName = or.Partner!.PartnerName,
                               OrderNumber = or.OrderNumber,
                               VisaNumber = or.VisaNumber,
                               Duration = DateTime.Now.Subtract(or.CreatedAt).Days,
                               Job = or.OrderCriteria!.JobTitle!.Value,
                               Sponsor = or.Sponsor!.FullName,
                               Age = (int)or.OrderCriteria.Age!,
                               Language = or.OrderCriteria.Language!.Value,
                               Expereince = or.OrderCriteria.Experience!.Value,
                               NoOfVisa = or.NumberOfVisa,
                               Religion = or.OrderCriteria.Religion!.Value

                           }).ToListAsync();

        return response;

    }

    public async Task<List<VisaExpiryResponseDTO>> GetExpiredVisa()
    {

        var response = await _context.Applicants
            .Where(app => app.OrderId != null && !app.IsDeleted)
            .Join(_context.Orders.Where(o => !o.IsDeleted), app => app.OrderId, o => o.Id, (app, o) => new { Applicant = app, Order = o })
            .Join(_context.CountryOperations, ao => ao.Order.Sponsor!.Address!.CountryId, co => co.CountryId, (ao, co) => new { ApplicantOrder = ao, CountryOperation = co })
            .Where(aoc => DateTime.Compare(aoc.ApplicantOrder.Order.CreatedAt.AddDays(aoc.CountryOperation.VisaExpiryDays), DateTime.Now) < 0)
            .Select(aoc => new VisaExpiryResponseDTO
            {
                EmployerName = aoc.ApplicantOrder.Order.Sponsor!.FullName,
                EmployerPhoneNumber = aoc.ApplicantOrder.Order.Sponsor.Address!.PhoneNumber,
                EmployeeName = aoc.ApplicantOrder.Applicant.FirstName,
                PassportNumber = aoc.ApplicantOrder.Applicant.PassportNumber,
                WorkingCountry = aoc.ApplicantOrder.Order.Sponsor.Address.Country!.Value,
                Sex = aoc.ApplicantOrder.Applicant.Gender.ToString(),
                VisaExpired = aoc.ApplicantOrder.Order.CreatedAt.AddDays(aoc.CountryOperation.VisaExpiryDays),
                DatePassed = (int)DateTime.Now.Subtract(aoc.ApplicantOrder.Order.CreatedAt.AddDays(aoc.CountryOperation.VisaExpiryDays)).TotalDays
            })
            .ToListAsync();

        return response;
    }

    public async Task<List<PenalityResponseDTO>> GetPenality()
    {

        var settings = await _context.CompanySettings.FirstOrDefaultAsync();
        var penaltyInterval = TimeSpan.FromDays(settings!.PenalityInterval);
        var response = _context.Applicants
            .Where(app => app.OrderId != null && app.TraveledApplicant == null && app.Order!.IsDeleted == false)
            .Include(app => app.Order)
            .Include(app => app.Order!.Sponsor)
            .Include(app => app.Order!.Partner)
            .AsEnumerable()
            .Where(app => app.Order!.CreatedAt.Add(penaltyInterval) < DateTime.Now)
            .Select(res => new PenalityResponseDTO
            {
                Customer = res.Order!.Partner!.PartnerName,
                Sponsor = res.Order.Sponsor!.FullName,
                Days = (int)(DateTime.Now - res.Order.CreatedAt.Add(penaltyInterval)).TotalDays,
                Penality = ((int)(DateTime.Now - res.Order.CreatedAt.Add(penaltyInterval)).TotalDays) * settings.PenalityAmount
            })
            .ToList();
        var res = response;

        return await Task.FromResult(response);

    }

    public async Task<List<ComplaintResponseDTO>> GetComplaints()
    {
        var response = await _context.Applicants
                       .Where(ap => ap.OrderId != null && ap.Complaints != null && ap.Complaints.Count() > 0 && ap.Complaints.Any(comp => comp.IsClosed) == false)
                       .Select(s => new ComplaintResponseDTO
                       {
                           Employee = s.FirstName,
                           Sponsor = s.Order!.Sponsor!.FullName,
                           Path = "api/complaint/get-order-complaints/" + s.Id

                       }).ToListAsync();
        return response;

    }

    public async Task<GetUnAssignedOrdersResponseDTO> GetUnAssignedOrdersDropDown()
    {

        var res = new GetUnAssignedOrdersResponseDTO();


        var response = await _context.Orders
                         .Where(or => or.Employees == null || or.Employees.Count() == 0)
                         .Select(or => new GetUnAssignedOrdersDropdownResponseDTO
                         {
                             OrderId = or.Id,
                             OrderNumber = or.OrderNumber,
                             SponsorName = or.Sponsor!.FullName,
                             VisaNumber = or.VisaNumber,
                             JobTitle = or.OrderCriteria!.JobTitle!.Value
                         }).ToListAsync();

        res.Order = response;
        return res;
    }
}