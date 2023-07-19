
using AppDiv.SmartAgency.Application.Contracts.DTOs.DashbourdDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class ApplicantProcessRepository : BaseRepository<ApplicantProcess>, IApplicantProcessRepository
    {
        private readonly SmartAgencyDbContext _context;

        public ApplicantProcessRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;

        }

        public async Task<object> GetDashbourdResult(DateTime? startDate, DateTime? endDate)
        {


            var response = new List<Object>();

            var groupedApplicantProcesses = await _context.ApplicantProcesses
                                   .Include(ap => ap.ProcessDefinition)
                                   .Where(ap => (ap.Status == ProcessStatus.In) && (ap.Date >= startDate && ap.Date <= endDate))
                                   .GroupBy(ap => ap.ProcessDefinitionId)
                                   .Select(g => new
                                   {
                                       Count = g.Count(),
                                       Name = g.FirstOrDefault().ProcessDefinition.Name
                                   }).ToListAsync();




            if (groupedApplicantProcesses != null)
            {
                foreach (var ga in groupedApplicantProcesses)
                {
                    response.Add(ga);
                }
            }

            // var numberOfAssignedVisa = await _context.Orders
            //                         .CountAsync(o => (o.Employees != null && o.Employees.Count > 0) && o.CreatedAt >= startDate && o.CreatedAt < endDate);

            var numberOfAssignedVisas = await _context.Applicants
                                    .CountAsync(app => (app.OrderId != null) && (app.Order.CreatedAt >= startDate && app.Order.CreatedAt < endDate));




            if (numberOfAssignedVisas > 0)
            {
                var assignedVisa = new
                {
                    Count = numberOfAssignedVisas,
                    Name = "Assigned Visa"
                };

                // if (groupedApplicantProcesses != null)
                // {
                //     groupedApplicantProcesses.Add(assignedVisa!);
                //     //response.Add(groupedApplicantProcesses);
                // }
                // else
                // {

                response.Add(assignedVisa);
                // }


            }


            return response;



        }


        public async Task<List<JObject>> GetQuickLinks()
        {
            var response = new List<JObject>();




            var complaints = await _context.Applicants
                    .CountAsync(ap => ap.OrderId != null && ap.Complaints != null && ap.Complaints.Count() > 0 && ap.Complaints.Any(comp => comp.IsClosed) == false);

            if (complaints > 0)
            {
                var complaint = new JObject();
                complaint["Name"] = "Complaints";
                complaint["Count"] = complaints;
                complaint["Path"] = "quick-links/get-complaint";

                response.Add(complaint);
            }

            var daysAgo = DateTime.Now.AddDays(-10);
            var newAssignedVisas = await _context.Applicants
                              .CountAsync(ap => ap.OrderId != null && ap.CreatedAt >= daysAgo);

            if (newAssignedVisas > 0)
            {
                var newAssignedVisa = new JObject();

                newAssignedVisa["Name"] = "New Assigned Visas";
                newAssignedVisa["Count"] = newAssignedVisas;
                newAssignedVisa["Path"] = "quick-links/get-new-assigned-visa";
                response.Add(newAssignedVisa);

            }




            var notProcessedApplicants = await _context.Applicants
                        .CountAsync(ap => ap.ApplicantProcesses == null || ap.ApplicantProcesses.Count() == 0);

            if (notProcessedApplicants > 0)
            {
                var notProcessedApplicant = new JObject();

                notProcessedApplicant["Name"] = "Not Processed Applicants";
                notProcessedApplicant["Count"] = notProcessedApplicants;
                notProcessedApplicant["Path"] = "quick-links/get-not-processed-applicant";

                response.Add(notProcessedApplicant);
            }
            var notAssignedVisas = await _context.Orders
                                     .CountAsync(or => or.Employees == null || or.Employees.Count == 0);
            if (notAssignedVisas > 0)
            {
                var notAssignedVisa = new JObject();

                notAssignedVisa["Name"] = "Not Assigned Visas";
                notAssignedVisa["Count"] = notAssignedVisas;
                notAssignedVisa["Path"] = "quick-links/get-not-assigned-visa";

                response.Add(notAssignedVisa);


            }

            var expiredVisas = await _context.Applicants
                        .Where(app => app.OrderId != null && app.IsDeleted == false)
                        .Join(_context.Orders.Where(o => !o.IsDeleted), app => app.OrderId, o => o.Id, (app, o) => new { Applicant = app, Order = o })
                        .Join(_context.CountryOperations, ao => ao.Order.Sponsor.Address.CountryId, co => co.CountryId, (ao, co) => new { ApplicantOrder = ao, CountryOperation = co })
                        .Where(aoc => DateTime.Compare(aoc.ApplicantOrder.Order.CreatedAt.AddDays(aoc.CountryOperation.VisaExpiryDays), DateTime.Now) < 0).CountAsync();

            if (expiredVisas > 0)
            {

                var expiredVisa = new JObject();

                expiredVisa["Name"] = "Expired Visas";
                expiredVisa["Count"] = expiredVisas;
                expiredVisa["Path"] = "quick-links/get-expired-visa";

                response.Add(expiredVisa);


            }



            var settings = await _context.CompanySettings.FirstOrDefaultAsync();

            if (settings != null)
            {
                var penaltyInterval = TimeSpan.FromDays(settings.PenalityInterval);

                var penalities = await _context.Applicants
                   .CountAsync(app => (app.IsDeleted == false) && (app.TraveledApplicant == null) && (app.OrderId != null) && (app.Order.IsDeleted == false) && (DateTime.Compare(app.Order.CreatedAt.AddDays(settings.PenalityInterval), DateTime.Now) < 0));


                if (penalities > 0)
                {
                    var penality = new JObject();

                    penality["Name"] = "Penality";
                    penality["Count"] = penalities;
                    penality["Path"] = "quick-links/get-penality";

                    response.Add(penality);

                }
            }


            //dynamic processes


            var expiredProcesses = await _context.ApplicantProcesses
                          .Include(ap => ap.ProcessDefinition)
                          .Where(ap => ap.Status == ProcessStatus.In)
                          .Where(ap => DateTime.UtcNow > ap.Date.AddDays(ap.ProcessDefinition.ExpiryInterval))
                          .GroupBy(ap => ap.ProcessDefinitionId)
                          .Select(g => new JObject(
                                          //new JProperty("Id", g.Key),
                                          new JProperty("Name", g.FirstOrDefault().ProcessDefinition.Name),
                                          new JProperty("Count", g.Count()),
                                          new JProperty("Path", "quick-links/get-dynamic-process/" + g.Key)
                                      )).ToListAsync();

            if (expiredProcesses != null)
            {
                foreach (var ePro in expiredProcesses)
                {
                    response.Add(ePro);
                }
            }



            return response;
        }

        public async Task<List<NavBarResponseDTO>> GetNavBars(DateTime? startDate, DateTime? endDate)
        {

            var response = new List<NavBarResponseDTO>();

            var totalApplicants = await _context.Applicants.CountAsync(app => (app.IsDeleted == false) && (app.CreatedAt >= startDate && app.CreatedAt < endDate));
            var requestedApplicants = await _context.Applicants.CountAsync(app => (app.IsDeleted == false) && (app.IsRequested == true) && (app.CreatedAt >= startDate && app.CreatedAt < endDate));
            var onlineApplicants = await _context.OnlineApplicants.CountAsync(oApp => (oApp.CreatedAt >= startDate && oApp.CreatedAt < endDate));
            var totalOrders = await _context.Orders.CountAsync(order => (order.IsDeleted == false) && (order.CreatedAt >= startDate && order.CreatedAt < endDate));
            //var travelledApplicants = await _context.TraveledApplicants.CountAsync(tApp => tApp.CreatedAt >= startDate && tApp.CreatedAt <= endDate);


            response.Add(new NavBarResponseDTO { Name = "Total Applicants", Count = totalApplicants });
            response.Add(new NavBarResponseDTO { Name = "Requested Applicants", Count = requestedApplicants });
            response.Add(new NavBarResponseDTO { Name = "Online Applicants", Count = onlineApplicants });
            response.Add(new NavBarResponseDTO { Name = "Total Orders", Count = totalOrders });
            //  response.Add(new NavBarResponseDTO { Name = "Travelled Applicants", Count = travelledApplicants });

            var res = response;
            return response;


        }


    }
}