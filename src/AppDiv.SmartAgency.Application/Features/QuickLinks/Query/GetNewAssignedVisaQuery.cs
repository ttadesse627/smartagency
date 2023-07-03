
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.QuickLinks.Query
{
    public class GetNewAssignedVisaQuery : IRequest<List<NewAssignedVisaResponseDTO>>
    {
        
    }

    public class GetNewAssignedVisaHandler: IRequestHandler<GetNewAssignedVisaQuery , List<NewAssignedVisaResponseDTO>>
    {
       private readonly IApplicantRepository _applicantRepository;

        public GetNewAssignedVisaHandler(IApplicantRepository applicantRepository)
        {
            _applicantRepository= applicantRepository;
        }

      public async Task<List<NewAssignedVisaResponseDTO>>  Handle(GetNewAssignedVisaQuery request, CancellationToken cancellationToken)
      {
          var response = await _applicantRepository.GetNewAssignedVisa();
          return response;
      }

    }
}