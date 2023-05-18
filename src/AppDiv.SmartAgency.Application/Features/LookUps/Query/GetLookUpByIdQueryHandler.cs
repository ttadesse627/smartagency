using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Query
{
    public class GetLookUpByIdQueryHandler : IRequestHandler<GetLookUpByIdQuery, LookUp>
    {
        private readonly ILookUpRepository _lookUpRepository;
        private readonly IMediator _mediator;

        public GetLookUpByIdQueryHandler(IMediator mediator, ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
            _mediator = mediator;
        }
        public async Task<LookUp> Handle(GetLookUpByIdQuery request, CancellationToken cancellationToken)
        {
            var lookUp = await _lookUpRepository.GetByIdAsync(request.Id);
            return lookUp;
        }
    }
}