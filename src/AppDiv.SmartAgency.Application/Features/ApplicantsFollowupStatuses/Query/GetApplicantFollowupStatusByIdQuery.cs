using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Query
{
    public class GetApplicantFollowupStatusByIdQuery: IRequest<GetApplicantFollowupStatusByIdResponseDTO>
    {
        public Guid Id { get; private set; }

        public GetApplicantFollowupStatusByIdQuery(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class GetApplicantFollowupStatusByIdHandler : IRequestHandler<GetApplicantFollowupStatusByIdQuery, GetApplicantFollowupStatusByIdResponseDTO>
    {
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;

        public GetApplicantFollowupStatusByIdHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository)
        {
            _applicantFollowupStatusRepository = applicantFollowupStatusRepository;
        }
        public async Task<GetApplicantFollowupStatusByIdResponseDTO> Handle(GetApplicantFollowupStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var selectedApplicantFollowupStatus = await _applicantFollowupStatusRepository.GetByIdAsync(request.Id);
            return CustomMapper.Mapper.Map<GetApplicantFollowupStatusByIdResponseDTO>(selectedApplicantFollowupStatus);
           
        }
}
}
