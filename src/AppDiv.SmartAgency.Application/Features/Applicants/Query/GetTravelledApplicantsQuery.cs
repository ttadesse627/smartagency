using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public class GetTravelledApplicantsQuery : IRequest<List<TravelledApplicantsResponseDTO>>
    {

    }
    public class GetTravelledApplicantsQueryHandler : IRequestHandler<GetTravelledApplicantsQuery, List<TravelledApplicantsResponseDTO>>
    {
        private readonly IApplicantRepository _applicantRepository;

        public GetTravelledApplicantsQueryHandler(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }
        public async Task<List<TravelledApplicantsResponseDTO>> Handle(GetTravelledApplicantsQuery request, CancellationToken cancellationToken)
        {
            return await _applicantRepository.GetTravelledApplicants();

        }

    }

}