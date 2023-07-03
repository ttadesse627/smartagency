
using System.Reflection.Metadata;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.QuickLinks.Query
{
    public class GetNotProcessedApplicantQuery :IRequest<List<NotProcessedApplicantResponseDTO>>
    {
        
    }

    public class GetNotProcessedApplicantHandler : IRequestHandler<GetNotProcessedApplicantQuery , List<NotProcessedApplicantResponseDTO>>
    {
       

       IApplicantRepository _applicantRepository;

       public GetNotProcessedApplicantHandler(IApplicantRepository applicantRepository)
       {
        _applicantRepository= applicantRepository;

       }

       public async Task<List<NotProcessedApplicantResponseDTO>> Handle(GetNotProcessedApplicantQuery request, CancellationToken cancellationToken)
       {
          var response= await _applicantRepository.GetNotProcessedApplicants();

          return response;

       }



    }
}