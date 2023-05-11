using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Applicants.OnlineApplicants
{
    public class GetOnlineApplicantByIdQuery: IRequest<OnlineApplicantResponseDTO>
    {
        public Guid Id { get; private set; }

        public GetOnlineApplicantByIdQuery(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class GetOnlineApplicantByIdHandler : IRequestHandler<GetOnlineApplicantByIdQuery, OnlineApplicantResponseDTO>
    {
        private readonly IMediator _mediator;

        public GetOnlineApplicantByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OnlineApplicantResponseDTO> Handle(GetOnlineApplicantByIdQuery request, CancellationToken cancellationToken)
        {
            var onlineApplicants = await _mediator.Send(new GetAllOnlineApplicantQuery());
            var selectedOnlineApplicant = onlineApplicants.FirstOrDefault(oa=>oa.Id == request.Id);
            return CustomMapper.Mapper.Map<OnlineApplicantResponseDTO>(selectedOnlineApplicant);
           
        }
    }
}