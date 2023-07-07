using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetTicketProcessApplicantsQuery() : IRequest<TicketProcessResponseDTO> { }
public class GetTicketProcessApplicantsQueryHandler : IRequestHandler<GetTicketProcessApplicantsQuery, TicketProcessResponseDTO>
{
    private readonly IProcessRepository _processRepository;
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketReadyRepository _ticketReadyRepository;

    public GetTicketProcessApplicantsQueryHandler(
        IApplicantRepository applicantRepository, IProcessRepository processRepository,
        IProcessDefinitionRepository definitionRepository, ITicketReadyRepository ticketReadyRepository)
    {
        _applicantRepository = applicantRepository;
        _processRepository = processRepository;
        _definitionRepository = definitionRepository;
        _ticketReadyRepository = ticketReadyRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(GetTicketProcessApplicantsQuery query, CancellationToken cancellationToken)
    {
        var applicantLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var pdLoadedProperties = new string[] {
                "ApplicantProcesses", "ApplicantProcesses.Applicant.Order",
                "ApplicantProcesses.Applicant.Order.Sponsor", "ApplicantProcesses.Applicant.DesiredCountry",
                "ApplicantProcesses.Applicant.Order.PortOfArrival",
            };

        var response = new TicketProcessResponseDTO();

            var onProcessApplicants = await _definitionRepository.GetAllWithPredicateAsync(
                pd => pd.ApplicantProcesses.All(applPr => applPr.Status == ProcessStatus.In) && pd.ProcessId == Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"), pdLoadedProperties);

            var ticketReady = onProcessApplicants.Where(appl => appl.Id.ToString() == "00fa1a8e-ac70-400e-8f37-20010f81a27a").FirstOrDefault();
            var ticketRegistration = onProcessApplicants.Where(appl => appl.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7").FirstOrDefault();
            var ticketRefund = onProcessApplicants.Where(appl => appl.Id.ToString() == "2d9ef769-6d03-4406-9849-430ff9723778").FirstOrDefault();
            var ticketRebook = onProcessApplicants.Where(appl => appl.Id.ToString() == "3048b353-039d-41b6-8690-a9aaa2e679cf").FirstOrDefault();
            var ticketRebookReg = onProcessApplicants.Where(appl => appl.Id.ToString() == "4048b353-039d-41b6-8690-a9aaa2e679cf").FirstOrDefault();
            var traveled = onProcessApplicants.Where(appl => appl.Id.ToString() == "5b912c00-9df3-47a1-a525-410abf239616").FirstOrDefault();

            var tkReadyApplicants = new List<GetTicketReadyApplicantsResponseDTO>();
            var tkRegApplicants = new List<GetTicketRegistrationApplicantsResponseDTO>();
            var tkRefundApplicants = new List<GetTicketRefundApplicantsResponseDTO>();
            var tkRebookApplicants = new List<GetTicketRebookApplicantsResponseDTO>();
            var tkRebRegApplicants = new List<GetTicketRegistrationApplicantsResponseDTO>();
            var traveledApplicants = new List<GetTraveledApplicantsResponseDTO>();

            foreach (var applicant in ticketReady.ApplicantProcesses)
            {
                tkReadyApplicants.Add(new GetTicketReadyApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!,
                    Country = applicant.Applicant.DesiredCountry?.Value,
                    PortOfArrival = applicant.Applicant.Order?.PortOfArrival?.Value
                });
            }

            foreach (var applicant in ticketRegistration.ApplicantProcesses)
            {
                tkRegApplicants.Add(new GetTicketRegistrationApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber
                });
            }

            foreach (var applicant in ticketRefund.ApplicantProcesses)
            {
                tkRefundApplicants.Add(new GetTicketRefundApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
                });
            }

            foreach (var applicant in ticketRebook.ApplicantProcesses)
            {
                tkRebookApplicants.Add(new GetTicketRebookApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
                });
            }

            foreach (var applicant in ticketRebookReg.ApplicantProcesses)
            {
                tkRebRegApplicants.Add(new GetTicketRegistrationApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber
                });
            }

            foreach (var applicant in traveled.ApplicantProcesses)
            {
                tkRebRegApplicants.Add(new GetTraveledApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    Date = applicant.Date
                });
            }

            response.TicketReadyApplicants = tkReadyApplicants;
            response.TicketRegistrationApplicants = tkRegApplicants;
            response.TicketRefundApplicants = tkRefundApplicants;
            response.TicketRebookApplicants = tkRebookApplicants;
            response.TicketRebookRegistrationApplicants = tkRebRegApplicants;
            response.TraveledApplicants = traveledApplicants;
        
        return response;
    }
}