using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketReadyCommand(SubmitTicketReadyRequest request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketReadyCommandHandler : IRequestHandler<SubmitTicketReadyCommand, TicketProcessResponseDTO>
{
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketReadyRepository _ticketReadyRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketReadyCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository,
    IProcessDefinitionRepository definitionRepository, ITicketReadyRepository ticketReadyRepository)
    {
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _definitionRepository = definitionRepository;
        _ticketReadyRepository = ticketReadyRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketReadyCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;

        var applPro = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ApplicantId == request.ApplicantId && appPr.ProcessDefinitionId.ToString() == "00fa1a8e-ac70-400e-8f37-20010f81a27a", "Applicant");
        var applicant1 = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcess", "Order.Sponsor", "Order.TicketOffice");
        var ticketOffice = await _lookupRepository.GetWithPredicateAsync(lk => lk.Id == request.TicketOfficeId);


        // Update the applicant status on the ApplicantProcess table
        applPro.Status = ProcessStatus.Out;

        var tickReg = await _definitionRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7", "ApplicantProcesses", "ApplicantProcesses.Applicant");
        var applProcess = new ApplicantProcess
        {
            Applicant = applicant1,
            ProcessDefinition = tickReg,
            Date = (DateTime)request.Date,
            Status = ProcessStatus.In
        };
        var tickReadyAppl = new TicketReady
        {
            DateInterval = request.DateInterval,
            TicketOffice = ticketOffice,
            Applicant = applicant1
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

        var pdLoadedProperties = new string[] {
                "ApplicantProcesses", "ApplicantProcesses.Applicant.Order",
                "ApplicantProcesses.Applicant.Order.Sponsor", "ApplicantProcesses.Applicant.DesiredCountry",
                "ApplicantProcesses.Applicant.Order.PortOfArrival",
            };

        var response = new TicketProcessResponseDTO();

        var proDefs = await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"), pdLoadedProperties);

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
            var onTciketReadyAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.Id == ticketReady.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketReadyAppls)
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
        }

        if (ticketRegistration != null)
        {
            var applProLoadedProps = new string[] { "Applicant" };
            var onTciketRegistrationAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.Id == ticketRegistration.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
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
            var onTciketRefundAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.Id == ticketRefund.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketRefundAppls)
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
        }

        if (ticketRebook != null)
        {
            var applProLoadedProps = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
            var onTciketRebookAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.Id == ticketRebook.Id && applPro.Status == ProcessStatus.In, applProLoadedProps);
            foreach (var applicant in onTciketRebookAppls)
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
        }

        if (ticketRebookReg != null)
        {
            var onTciketRebookRegAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.Id == ticketRebookReg.Id && applPro.Status == ProcessStatus.In, "Applicant");
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
            var traveledAppls = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.Id == traveled.Id && applPro.Status == ProcessStatus.In, "Applicant");
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