
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketRebookCommand(SubmitTicketRebookRequest Request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketRebookCommandHandler : IRequestHandler<SubmitTicketRebookCommand, TicketProcessResponseDTO>
{
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketRebookRepository _ticketRebookRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketRebookCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository,
    IProcessDefinitionRepository proDefRepository, ITicketRebookRepository ticketRebookRepository)
    {
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
        _ticketRebookRepository = ticketRebookRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketRebookCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var applPro = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ApplicantId == request.ApplicantId && appPr.ProcessDefinitionId.ToString() == "3048b353-039d-41b6-8690-a9aaa2e679cf");
        var applicant1 = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId);


        // Update the applicant status on the ApplicantProcess table
        applPro.Status = ProcessStatus.Out;

        var tktRebook = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "4048b353-039d-41b6-8690-a9aaa2e679cf");
        var appProList = new List<ApplicantProcess>();
        var applTickRebookProcess = new ApplicantProcess
        {
            Applicant = applicant1,
            ProcessDefinition = tktRebook,
            Date = request.Date,
            Status = ProcessStatus.In
        };

        var tickRebook = new TicketRebook
        {
            DateInterval = request.DateInterval,
            Applicant = applicant1
        };

        try
        {
            await _applicantProcessRepository.InsertAsync(applTickRebookProcess, cancellationToken);
            await _ticketRebookRepository.InsertAsync(tickRebook, cancellationToken);
            await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            await _ticketRebookRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException(ex.Message);
        }

        var pdLoadedProperties = new string[] {
                "ApplicantProcesses", "ApplicantProcesses.Applicant.Order",
                "ApplicantProcesses.Applicant.Order.Sponsor", "ApplicantProcesses.Applicant.DesiredCountry",
                "ApplicantProcesses.Applicant.Order.PortOfArrival",
            };

        var response = new TicketProcessResponseDTO();

        var proDefs = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"), pdLoadedProperties);

        var ticketReady = proDefs.Where(pd => pd.Id.ToString() == "00fa1a8e-ac70-400e-8f37-20010f81a27a").FirstOrDefault();
        var ticketRegistration = proDefs.Where(appl => appl.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7").FirstOrDefault();
        var ticketRefund = proDefs.Where(appl => appl.Id.ToString() == "2d9ef769-6d03-4406-9849-430ff9723778").FirstOrDefault();
        var ticketRebook = proDefs.Where(appl => appl.Id.ToString() == "3048b353-039d-41b6-8690-a9aaa2e679cf").FirstOrDefault();
        var ticketRebookReg = proDefs.Where(appl => appl.Id.ToString() == "4048b353-039d-41b6-8690-a9aaa2e679cf").FirstOrDefault();
        var traveled = proDefs.Where(appl => appl.Id.ToString() == "5b912c00-9df3-47a1-a525-410abf239616").FirstOrDefault();

        var tkReadyApplicants = new List<GetTicketReadyApplicantsResponseDTO>();
        var tkRegApplicants = new List<GetTicketRegistrationApplicantsResponseDTO>();
        var tkRefundApplicants = new List<GetTicketRefundApplicantsResponseDTO>();
        var tkRebookApplicants = new List<GetTicketRebookApplicantsResponseDTO>();
        var tkRebRegApplicants = new List<GetTicketRegistrationApplicantsResponseDTO>();
        var traveledApplicants = new List<GetTraveledApplicantsResponseDTO>();

        if (ticketReady != null)
        {
            var applProLoadedProps = new string[] { "Applicant.Order", "Applicant.Order.Sponsor", "Applicant.Order.PortOfArrival" };
            var onTciketReadyAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ProcessDefinitionId == ticketReady.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketReadyAppls)
            {
                tkReadyApplicants.Add(new GetTicketReadyApplicantsResponseDTO()
                {
                    ApplicantId = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!,
                    Country = applicant.Applicant.DesiredCountry?.Value,
                    PortOfArrival = applicant.Applicant.Order?.PortOfArrival?.Value
                });
            }
        }

        if (ticketRegistration != null)
        {
            var applProLoadedProps = new string[] { "Applicant" };
            var onTciketRegistrationAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ProcessDefinitionId == ticketRegistration.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketRegistrationAppls)
            {
                tkRegApplicants.Add(new GetTicketRegistrationApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber
                });
            }
        }

        if (ticketRefund != null)
        {
            var applProLoadedProps = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
            var onTciketRefundAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ProcessDefinitionId == ticketRefund.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketRefundAppls)
            {
                tkRefundApplicants.Add(new GetTicketRefundApplicantsResponseDTO()
                {
                    ApplicantId = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
                });
            }
        }

        if (ticketRebook != null)
        {
            var applProLoadedProps = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
            var onTciketRebookAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ProcessDefinitionId == ticketRebook.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketRebookAppls)
            {
                tkRebookApplicants.Add(new GetTicketRebookApplicantsResponseDTO()
                {
                    ApplicantId = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
                });
            }
        }

        if (ticketRebookReg != null)
        {
            var onTciketRebookRegAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ProcessDefinitionId == ticketRebookReg.Id && applPro.Status == ProcessStatus.In, "Applicant");
            foreach (var applicant in onTciketRebookRegAppls)
            {
                tkRebRegApplicants.Add(new GetTicketRegistrationApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                });
            }
        }

        if (traveled != null)
        {
            var traveledAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ProcessDefinitionId == traveled.Id && applPro.Status == ProcessStatus.In, "Applicant");
            foreach (var applicant in traveledAppls)
            {
                tkRebRegApplicants.Add(new GetTraveledApplicantsResponseDTO()
                {
                    Id = applicant.Applicant.Id,
                    PassportNumber = applicant.Applicant.PassportNumber,
                    FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                    Date = applicant.Date
                });
            }
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