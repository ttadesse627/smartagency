
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Dashbourds.Query
{
    public class GetDashbourdQuery(DateTime? startDate, DateTime? endDate) : IRequest<Dictionary<string, Object>>
    {
        public DateTime? StartDate { get; set; } = startDate;
        public DateTime? EndDate { get; set; } = endDate;
    }

    public class GetDashbourdHandler(IProcessDefinitionRepository processDefinitionRepository, IApplicantProcessRepository applicantProcessRepository) : IRequestHandler<GetDashbourdQuery, Dictionary<string, Object>>
    {

        private readonly IProcessDefinitionRepository _processDefinitionRepository = processDefinitionRepository;
        private readonly IApplicantProcessRepository _applicantProcessRepository = applicantProcessRepository;

        public async Task<Dictionary<string, Object>> Handle(GetDashbourdQuery request, CancellationToken cancellationToken)
        {

            var response = new Dictionary<string, object>
                            {
                                { "thisWeekSummary", "" },
                                { "thisMonthSummary", "" },
                                { "quickLinks", ""},
                                { "navBars", ""}
                            };
            DateTime today = DateTime.Today;
            DateTime startOfMonth = new(today.Year, today.Month, 1);
            DateTime endDate = new(today.Year, today.Month, today.Day + 1);
            if (request.StartDate == null || request.EndDate == null)
            {
                request.StartDate = startOfMonth;
                request.EndDate = endDate;
            }


            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);





            var ThisWeekSummary = await _applicantProcessRepository.GetDashbourdResult(startOfWeek, endOfWeek);

            var ThisMonthSummary = await _applicantProcessRepository.GetDashbourdResult(request.StartDate, request.EndDate);
            var QuickLinks = await _applicantProcessRepository.GetQuickLinks();
            var NavBars = await _applicantProcessRepository.GetNavBars(request.StartDate, request.EndDate);


            //  response["thisWeekSummary"] = ThisWeekSummary;
            response["thisMonthSummary"] = ThisMonthSummary;
            response["quickLinks"] = QuickLinks;
            response["navBars"] = NavBars;

            var res = response;
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

