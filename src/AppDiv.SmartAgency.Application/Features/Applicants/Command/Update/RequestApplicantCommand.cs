

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public record RequestApplicantCommand(SendApplicantRequest Request) : IRequest<ServiceResponse<Int32>> { }
public class RequestApplicantCommandHandler : IRequestHandler<RequestApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IPartnerRepository _partnerRepository;
    private readonly IRequestedApplicantRepository _requestedApplicantRepository;
    public RequestApplicantCommandHandler(IApplicantRepository applicantRepository, IPartnerRepository partnerRepository, IRequestedApplicantRepository requestedApplicantRepository)
    {
        _applicantRepository = applicantRepository;
        _partnerRepository = partnerRepository;
        _requestedApplicantRepository = requestedApplicantRepository;
    }
    public async Task<ServiceResponse<int>> Handle(RequestApplicantCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var applicant = await _applicantRepository.GetAsync(command.Request.ApplicantId);
        var partner = await _partnerRepository.GetAsync(command.Request.PartnerId);
        if (applicant != null && partner != null)
        {
            var requestedApplicant = new RequestedApplicant
            {
                Applicant = applicant,
                Partner = partner
            };

            await _requestedApplicantRepository.InsertAsync(requestedApplicant, cancellationToken);
            try
            {
                var success = await _requestedApplicantRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        else
        {
            throw new NotFoundException($"Applicant or partner is not found!");
        }
        return response;
    }
}