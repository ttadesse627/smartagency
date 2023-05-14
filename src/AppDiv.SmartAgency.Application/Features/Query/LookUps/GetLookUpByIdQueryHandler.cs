using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetLookUpByIdQueryHandler : IRequestHandler<GetLookUpByIdQuery, LookUp>
    {
        private readonly IMediator _mediator;

        public GetLookUpByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<LookUp> Handle(GetLookUpByIdQuery request, CancellationToken cancellationToken)
        {
            var lookUps = await _mediator.Send(new GetAllLookUps(request.PageNumber, request.PageSize, request.SearchTerm, request.SearchByColumnName, request.OrderBy, request.SortingDirection));
            // var selectedLookUp = lookUps.Items.FirstOrDefault(l=>l.Id == request.Id);
            return CustomMapper.Mapper.Map<LookUp>(lookUps);
        }
    }
}