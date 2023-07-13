

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public class GetForEnjazQuery : IRequest<List<DropdownEnjazResponseDTO>>
{ }
public class GetForEnjazQueryHandler : IRequestHandler<GetForEnjazQuery, List<DropdownEnjazResponseDTO>>

{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IProcessRepository _processRepository;

    public GetForEnjazQueryHandler(IProcessRepository processRepository, IApplicantRepository applicantRepository)
    {
        _processRepository = processRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<List<DropdownEnjazResponseDTO>> Handle(GetForEnjazQuery request, CancellationToken cancellationToken)
    {
        var response = new List<DropdownEnjazResponseDTO>();
        var ordEagerLoadedProps = new string[]{"Sponsor","OrderCriteria.Language","OrderCriteria.Religion",
                                        "Employees","Employees.Jobtitle","Employees.Language","Enjaz"};

        var applEagerLoadedProps = new string[] { "Order", "Order.Sponsor", "Jobtitle", "Language", "Religion", "Enjaz" };

        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => app.OrderId != null);

        if (applicants != null && applicants.Any())
        {
            var enjazRequiredProcess = await _processRepository.GetEnjazRequiredProcessesAsync();
            if (enjazRequiredProcess.Any())
            {
                var enjazMinStep = enjazRequiredProcess.Min(pr => pr.Step);
                var prevProcesses = await _processRepository.GetPrevStepEnjazProcessesAsync(pr => pr.Step == enjazMinStep - 1);
                if (prevProcesses.Any())
                {
                    var prevProcess = prevProcesses.First();
                    if (prevProcess.ProcessDefinitions != null && prevProcess.ProcessDefinitions.Any())
                    {
                        var maxStep = prevProcess.ProcessDefinitions.Max(p => p.Step);
                        var lastPds = prevProcess.ProcessDefinitions.Where()
                        //////////////////
                        foreach (var empl in order.Employees)
                        {
                            var ordResp = new DropdownEnjazResponseDTO
                            {
                                OrderId = order.Id,
                                OrderNumber = order.OrderNumber,
                                SponsorFullName = order.Sponsor?.FullName,
                                EmployeeProfession = empl.Jobtitle.Value,
                                EmployeeLanguage = empl.Language.Value,
                                PassportNumber = empl.PassportNumber,
                                EmployeeFullName = empl.FirstName + " " + empl.MiddleName + " " + empl.LastName
                            };
                            response.Add(ordResp);
                        }
                    }

                }
            }

            //////////////////////////////////////////////////////////////////

            var orderList = await _orderRepository.GetAllWithPredicateAsync
                            (
                                order => order.IsDeleted == false && order.Employees != null && order.Employees.Count > 0 && order.Enjaz == null, ordEagerLoadedProps
                            );

            var applicantList = await _applicantRepository.GetAllWithPredicateAsync
                            (
                                applicant => applicant.IsDeleted == false && applicant.OrderId != null, applEagerLoadedProps
                            );

            if (orderList.Count > 0 && orderList != null)
            {
                foreach (var order in orderList)
                {
                    if (order.Employees != null && order.Employees.Count > 0)
                    {
                        foreach (var empl in order.Employees)
                        {
                            var ordResp = new DropdownEnjazResponseDTO
                            {
                                OrderId = order.Id,
                                OrderNumber = order.OrderNumber,
                                SponsorFullName = order.Sponsor?.FullName,
                                EmployeeProfession = empl.Jobtitle.Value,
                                EmployeeLanguage = empl.Language.Value,
                                PassportNumber = empl.PassportNumber,
                                EmployeeFullName = empl.FirstName + " " + empl.MiddleName + " " + empl.LastName
                            };
                            response.Add(ordResp);
                        }
                    }

                }
            }


            return response;
        }
    }