
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
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
           

            var response= new List<Object>();

            var applicantProcesse= await _context.ApplicantProcesses
                                   .Include(ap=> ap.ProcessDefinition)
                                   .ToListAsync();

             var groupedApplicantProcesses  = applicantProcesse
                            .Where(ap => ap.Date >= startDate && ap.Date <=endDate)
                            .GroupBy(ap => ap.ProcessDefinitionId)
                            .Select(g => new { //ProcessDefinitionId = g.Key,
                                                Count = g.Count(),                       
                                                Name = g.FirstOrDefault()?.ProcessDefinition!.Name
                              }).ToList();

                   
            


                            var numberOfAssignedVisa = await _context.Orders
                                                    .CountAsync(o => o.EmployeeId != null && o.CreatedAt >= startDate && o.CreatedAt < endDate);


                            if(groupedApplicantProcesses!= null)
                            {
                                foreach( var ga in groupedApplicantProcesses){
                                    response.Add(ga);
                                }
                            } 
                                                    
                        

                        if (numberOfAssignedVisa > 0)
                        {
                            var assignedVisa = new { 
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


                  





                   //  var assignedVisa = await _context.Orders
                    //                     .Where(o => o.EmployeeId != null && o.CreatedAt >= startDate && o.CreatedAt < endDate)
                    //                     .Select( av => new {  //OrderId= av.Key,
                    //                                           Count= av.Count(),
                    //                                           Name = "Assigned Visa"
                    //                                           }).ToListAsync();           
                



                  
                                    // var numberOfAssignedVisa = await _context.Orders
                                    //     .Where(o => o.EmployeeId != null && o.CreatedAt >= startDate && o.CreatedAt < endDate)
                                    //     .Select(o => new {
                                    //         // OrderId = o.Id,
                                    //         Count = orderCount,
                                    //         Name = "Assigned Visa"
                                    //     })
                                    //     .ToListAsync();            



        }


                   public Task<object> GetQuickLinks()
                    {
                        var response= new List<Object>();


                        throw new NotImplementedException();

                        return null;
                    }

      
    }
}