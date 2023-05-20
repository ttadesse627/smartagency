using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDiv.SmartAgency.Application.Mapper;
using MediatR;
/*
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
        private readonly IMediator _mediator;

        public GetApplicantFollowupStatusByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<GetApplicantFollowupStatusByIdResponseDTO> Handle(GetApplicantFollowupStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var applicantFollowupStatuses = await _mediator.Send(new GetAllApplicantFollowupStatusQuery());
            var selectedApplicantFollowupStatus = applicantFollowupStatuses.FirstOrDefault(d=>d.Id == request.Id);
            return CustomMapper.Mapper.Map<GetApplicantFollowupStatusByIdResponseDTO>(selectedApplicantFollowupStatus);
           
        }
}
}
*/