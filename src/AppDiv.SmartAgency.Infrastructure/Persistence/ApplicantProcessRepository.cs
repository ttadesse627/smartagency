
using System;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Features.QuickLinks.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

            var applicantProcesse = await _context.ApplicantProcesses
                                   .Include(ap => ap.ProcessDefinition)
                                   .ToListAsync();

            var groupedApplicantProcesses = applicantProcesse
                           .Where(ap => ap.Date >= startDate && ap.Date <= endDate)
                           .GroupBy(ap => ap.ProcessDefinitionId)
                           .Select(g => new
                           { //ProcessDefinitionId = g.Key,
                               Count = g.Count(),
                               Name = g.FirstOrDefault()?.ProcessDefinition!.Name
                           }).ToList();





            var numberOfAssignedVisa = await _context.Orders
                                    .CountAsync(o => (o.Employees != null && o.Employees.Count > 0) && o.CreatedAt >= startDate && o.CreatedAt < endDate);


            if (groupedApplicantProcesses != null)
            {
                foreach (var ga in groupedApplicantProcesses)
                {
                    response.Add(ga);
                }
            }



            if (numberOfAssignedVisa > 0)
            {
                var assignedVisa = new
                {
                    Count = numberOfAssignedVisa,
                    Name = "Assigned Visa"
                };

                if (groupedApplicantProcesses != null)
                {
                    groupedApplicantProcesses.Add(assignedVisa!);
                }
                else
                {

                    response.Add(assignedVisa);
                }


            }


            return response;



        }


        public async Task<List<QuickLinksResponseDTO>> GetQuickLinks()
        {
            var response = new List<QuickLinksResponseDTO>();
            var complaints = await _context.Complaints
                            .Where(c => c.IsClosed == false && c.OrderId != null).CountAsync();

            if (complaints > 0)
            {
                var complaint = new QuickLinksResponseDTO
                {
                    Name = "Complaints",
                    Count = complaints,
                    Path = "api/quick-links/get-complaint"
                };
                response.Add(complaint);
            }

            var daysAgo = DateTime.Now.AddDays(-10);
            var newAssignedVisas = await _context.Applicants
                              .Where(ap => ap.OrderId != null && ap.CreatedAt >= daysAgo).CountAsync();

            if (newAssignedVisas > 0)
            {
                var newAssignedVisa = new QuickLinksResponseDTO
                {
                    Name = "New Assigned Visas",
                    Count = newAssignedVisas,
                    Path = "api/quick-links/get-new-assigned-visa"
                };
                response.Add(newAssignedVisa);

            }

            var notProcessedApplicants = await _context.Applicants
                        .Where(ap => ap.ApplicantProcesses == null || ap.ApplicantProcesses.Count() == 0).CountAsync();

            if (notProcessedApplicants > 0)
            {
                var notProcessedApplicant = new QuickLinksResponseDTO
                {
                    Name = "Not Processed Applicants",
                    Count = notProcessedApplicants,
                    Path = "api/quick-links/get-not-processed-applicant"
                };
                response.Add(notProcessedApplicant);
            }
            var notAssignedVisas = await _context.Orders
                                     .Where(or => or.Employees == null).CountAsync();
            if (notAssignedVisas > 0)
            {
                var notAssignedVisa = new QuickLinksResponseDTO
                {
                    Name = "Not Assigned Visas",
                    Count = notAssignedVisas,
                    Path = "api/quick-links/get-not-assigned-visa"
                };
                response.Add(notAssignedVisa);


            }

            var expiredVisas = await _context.Applicants
                        .Where(app => app.OrderId != null && !app.IsDeleted)
                        .Join(_context.Orders.Where(o => !o.IsDeleted), app => app.OrderId, o => o.Id, (app, o) => new { Applicant = app, Order = o })
                        .Join(_context.CountryOperations, ao => ao.Order.Sponsor.Address.CountryId, co => co.CountryId, (ao, co) => new { ApplicantOrder = ao, CountryOperation = co })
                        .Where(aoc => DateTime.Compare(aoc.ApplicantOrder.Order.CreatedAt.AddDays(aoc.CountryOperation.VisaExpiryDays), DateTime.Now) < 0).CountAsync();

            if (expiredVisas > 0)
            {

                var expiredVisa = new QuickLinksResponseDTO
                {
                    Name = "Expired Visas",
                    Count = expiredVisas,
                    Path = "api/quick-links/get-expired-visa"
                };
                response.Add(expiredVisa);


            }



            var settings = await _context.CompanySettings.FirstOrDefaultAsync();

            var penaltyInterval = TimeSpan.FromDays(settings.PenalityInterval);

            var penalities = _context.Applicants
               .Where(app => app.OrderId != null && app.TraveledApplicant == null)
               .Join(_context.Orders.Where(o => o.IsDeleted == false), app => app.OrderId, o => o.Id, (app, o) => new { Applicant = app, Order = o })
               .AsEnumerable()
               .Where(ao => ao.Order.CreatedAt.Add(penaltyInterval) <= DateTime.Now).Count();

            if (penalities > 0)
            {
                var penality = new QuickLinksResponseDTO
                {
                    Name = "Penality",
                    Count = penalities,
                    Path = "api/quick-links/get-penality"
                };
                response.Add(penality);

            }


            //dynamic processes



            var expiredProcesses = await _context.ApplicantProcesses
                .Include(ap => ap.ProcessDefinition.Process)
                .Include(ap => ap.ProcessDefinition)
                .Where(ap => DateTime.UtcNow > ap.Date.AddDays(ap.ProcessDefinition.ExpiryInterval))
                .GroupBy(ap => ap.ProcessDefinition.ProcessId)
                .Select(g => new QuickLinksResponseDTO
                {
                    Name = g.First().ProcessDefinition.Process.Name,
                    Count = g.Count(),
                    Path = "api/quick-links/get-daynamic-process"
                })
                .ToListAsync();
            if (expiredProcesses != null)
            {
                foreach (var ePro in expiredProcesses)
                {
                    response.Add(ePro);
                }
            }








            // aoc => DateTime.Compare(aoc.ApplicantOrder.Order.CreatedAt.AddDays(aoc.CountryOperation.VisaExpiryDays), DateTime.Now) < 0
            // var groupedApplicantProcesses = applicantProcesse
            //                            .Where(ap => ap.Date >= startDate && ap.Date <= endDate)
            //                            .GroupBy(ap => ap.ProcessDefinitionId)
            //                            .Select(g => new
            //                            { //ProcessDefinitionId = g.Key,
            //                                Count = g.Count(),
            //                                Name = g.FirstOrDefault()?.ProcessDefinition!.Name
            //                            }).ToList();



            return response;
        }


    }
}