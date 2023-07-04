using System.Reflection.PortableExecutable;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;

namespace AppDiv.SmartAgency.Application.Features.Dashbourds.Query
{
    public class GetDashbourdQuery: IRequest<Dictionary<string,Object>>
    {
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;} 
        public GetDashbourdQuery(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;

        }
        
    }

    public class GetDashbourdHandler: IRequestHandler<GetDashbourdQuery , Dictionary<string,Object>>
    {

       private readonly  IProcessDefinitionRepository _processDefinitionRepository;
       private readonly  IApplicantProcessRepository _applicantProcessRepository;
        private object applicantProcessRepository;

        public GetDashbourdHandler(IProcessDefinitionRepository processDefinitionRepository, IApplicantProcessRepository applicantProcessRepository)
       {
          _processDefinitionRepository= processDefinitionRepository;  
          _applicantProcessRepository= applicantProcessRepository;
       }

       public async Task<Dictionary<string,Object>> Handle(GetDashbourdQuery request, CancellationToken cancellationToken)
       {

                  var response = new Dictionary<string, object>
                            {
                                { "thisWeekSummary", null },
                                { "thisMonthSummary", null },
                                { "quickLinks", null},
                            };
                                  DateTime today = DateTime.Today;
                              DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                              DateTime endDate = new DateTime(today.Year, today.Month, today.Day + 1);
                        if (request.StartDate==null || request.EndDate==null)
                        {
                            request.StartDate= startOfMonth;
                            request.EndDate= endDate;
                        }

          
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);

        
          


           var  ThisWeekSummary = await _applicantProcessRepository.GetDashbourdResult(startOfWeek, endOfWeek); 
           
           var ThisMonthSummary = await _applicantProcessRepository.GetDashbourdResult(request.StartDate, request.EndDate); 
           var  QuickLinks= await _applicantProcessRepository.GetQuickLinks();


               response["thisWeekSummary"] = ThisWeekSummary;
               response["thisMonthSummary"] = ThisMonthSummary;
                 response["quickLinks"] = QuickLinks;

        var res=response;
            return response;   

      }


  /* var groupedApplicantProcesses = applicantProcesses
        .Where(ap => ap.CreatedAt >= startOfWeek && ap.CreatedAt < endOfWeek)
        .GroupBy(ap => ap.ProcessDefinitionId)
        .Select(g => new { 
            ProcessDefinitionId = g.Key, 
            Count = g.Count(),
            Name = _context.ProcessDefinitions
                .Where(pd => pd.ProcessDefinitionId == g.Key)
                .Select(pd => pd.Name)
                .SingleOrDefault()
        });

*/

        

      
        // return groupedApplicantProcesses.ToList();
        
    




   
       }   
    }

