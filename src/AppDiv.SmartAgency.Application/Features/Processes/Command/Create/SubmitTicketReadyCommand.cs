using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record SubmitTicketReadyCommand(SubmitTicketReadyRequest request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketReadyCommandHandler : IRequestHandler<SubmitTicketReadyCommand, TicketProcessResponseDTO>
{
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketReadyRepository _ticketReadyRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketReadyCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository,
    IProcessDefinitionRepository proDefRepository, ITicketReadyRepository ticketReadyRepository)
    {
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
        _ticketReadyRepository = ticketReadyRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketReadyCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new TicketProcessResponseDTO();

        var applPro = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ApplicantId == request.ApplicantId && appPr.ProcessDefinitionId.ToString() == "00fa1a8e-ac70-400e-8f37-20010f81a27a", "Applicant");
        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcess", "Order.Sponsor", "Order.TicketOffice");
        var ticketOffice = await _lookupRepository.GetWithPredicateAsync(lk => lk.Id == request.TicketOfficeId);


        // Update the applicant status on the ApplicantProcess table
        applPro.Status = ProcessStatus.Out;

        var tickReg = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7", "ApplicantProcesses", "ApplicantProcesses.Applicant");
        var applProcess = new ApplicantProcess
        {
            Applicant = applicant,
            ProcessDefinition = tickReg,
            Date = request.Date,
            Status = ProcessStatus.In
        };
        var tickReadyAppl = new TicketReady
        {
            DateInterval = request.DateInterval,
            TicketOffice = ticketOffice,
            ApplicantProcess = applPro
        };

        try
        {
            await _applicantProcessRepository.InsertAsync(applProcess, cancellationToken);
            await _ticketReadyRepository.InsertAsync(tickReadyAppl, cancellationToken);
            await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            await _ticketReadyRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException(ex.Message);
        }

        var pDefs = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"));
        var processDefinitions = pDefs.OrderBy(pd => pd.Step).ToList();

        var pdLoadedProperties = new string[] {
                "ApplicantProcesses", "ApplicantProcesses.Applicant.Order",
                "ApplicantProcesses.Applicant.Order.Sponsor", "ApplicantProcesses.Applicant.DesiredCountry",
                "ApplicantProcesses.Applicant.Order.PortOfArrival",
            };


        var ticketProcessApplicants = await _proDefRepository.GetAllWithPredicateAsync(
            pd => pd.ApplicantProcesses.All(applPr => applPr.Status == ProcessStatus.In) && pd.ProcessId == Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"), pdLoadedProperties);

        var ticketReady = ticketProcessApplicants.Where(appl => appl.Id.ToString() == "00fa1a8e-ac70-400e-8f37-20010f81a27a").First();
        var ticketRegistration = ticketProcessApplicants.Where(appl => appl.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7").First();
        var ticketRefund = ticketProcessApplicants.Where(appl => appl.Id.ToString() == "2d9ef769-6d03-4406-9849-430ff9723778").First();
        var ticketRebook = ticketProcessApplicants.Where(appl => appl.Id.ToString() == "3048b353-039d-41b6-8690-a9aaa2e679cf").First();
        var ticketRebookReg = ticketProcessApplicants.Where(appl => appl.Id.ToString() == "4048b353-039d-41b6-8690-a9aaa2e679cf").First();
        var traveled = ticketProcessApplicants.Where(appl => appl.Id.ToString() == "5b912c00-9df3-47a1-a525-410abf239616").First();

        var tkReadyApplicants = new List<GetTicketReadyApplicantsResponseDTO>();
        var tkRegApplicants = new List<GetTicketRegistrationApplicantsResponseDTO>();
        var tkRefundApplicants = new List<GetTicketRefundApplicantsResponseDTO>();
        var tkRebookApplicants = new List<GetTicketRebookApplicantsResponseDTO>();
        var tkRebRegApplicants = new List<GetTicketRegistrationApplicantsResponseDTO>();
        var traveledApplicants = new List<GetTraveledApplicantsResponseDTO>();

        foreach (var appl in ticketReady.ApplicantProcesses)
        {
            tkReadyApplicants.Add(new GetTicketReadyApplicantsResponseDTO()
            {
                Id = appl.Id,
                PassportNumber = appl.Applicant.PassportNumber,
                FullName = appl.Applicant.FirstName + " " + appl.Applicant.MiddleName + " " + appl.Applicant.LastName,
                OrderNumber = appl.Applicant.Order?.OrderNumber!,
                SponsorName = appl.Applicant.Order?.Sponsor?.FullName!,
                Country = appl.Applicant.DesiredCountry?.Value,
                PortOfArrival = appl.Applicant.Order?.PortOfArrival?.Value
            });
        }

        foreach (var appl in ticketRegistration.ApplicantProcesses)
        {
            tkRegApplicants.Add(new GetTicketRegistrationApplicantsResponseDTO()
            {
                Id = appl.Applicant.Id,
                PassportNumber = appl.Applicant.PassportNumber
            });
        }

        foreach (var appl in ticketRefund.ApplicantProcesses)
        {
            tkRefundApplicants.Add(new GetTicketRefundApplicantsResponseDTO()
            {
                Id = appl.Applicant.Id,
                PassportNumber = appl.Applicant.PassportNumber,
                FullName = appl.Applicant.FirstName + " " + appl.Applicant.MiddleName + " " + appl.Applicant.LastName,
                OrderNumber = appl.Applicant.Order?.OrderNumber!,
                SponsorName = appl.Applicant.Order?.Sponsor?.FullName!
            });
        }

        foreach (var appl in ticketRebook.ApplicantProcesses)
        {
            tkRebookApplicants.Add(new GetTicketRebookApplicantsResponseDTO()
            {
                Id = appl.Applicant.Id,
                PassportNumber = appl.Applicant.PassportNumber,
                FullName = appl.Applicant.FirstName + " " + appl.Applicant.MiddleName + " " + appl.Applicant.LastName,
                OrderNumber = appl.Applicant.Order?.OrderNumber!,
                SponsorName = appl.Applicant.Order?.Sponsor?.FullName!
            });
        }

        foreach (var appl in ticketRebookReg.ApplicantProcesses)
        {
            tkRebRegApplicants.Add(new GetTicketRegistrationApplicantsResponseDTO()
            {
                Id = appl.Applicant.Id,
                PassportNumber = appl.Applicant.PassportNumber
            });
        }

        foreach (var appl in traveled.ApplicantProcesses)
        {
            tkRebRegApplicants.Add(new GetTraveledApplicantsResponseDTO()
            {
                Id = appl.Applicant.Id,
                PassportNumber = appl.Applicant.PassportNumber,
                FullName = appl.Applicant.FirstName + " " + appl.Applicant.MiddleName + " " + appl.Applicant.LastName,
                Date = appl.Date
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