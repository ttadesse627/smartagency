using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.ApplicantFollowupStatuses
{
    public class GetAllApplicantFollowupStatusQuery: IRequest<List<ApplicantFollowupStatusResponseDTO>>
    {

    }

    public class GetAllApplicantFollowupStatusHandler : IRequestHandler<GetAllApplicantFollowupStatusQuery, List<ApplicantFollowupStatusResponseDTO>>
    {
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;

        public GetAllApplicantFollowupStatusHandler(IApplicantFollowupStatusRepository applicantFollowupStatusQueryRepository)
        {
            _applicantFollowupStatusRepository = applicantFollowupStatusQueryRepository;
        }
        public async Task<List<ApplicantFollowupStatusResponseDTO>> Handle(GetAllApplicantFollowupStatusQuery request, CancellationToken cancellationToken)
        {
            var applicantFollowupStatusList = await _applicantFollowupStatusRepository.GetAllWithAsync("Applicant","FollowupStatus");
            var applicantFollowupStatusResponse = CustomMapper.Mapper.Map<List<ApplicantFollowupStatusResponseDTO>>(applicantFollowupStatusList);
            return applicantFollowupStatusResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}