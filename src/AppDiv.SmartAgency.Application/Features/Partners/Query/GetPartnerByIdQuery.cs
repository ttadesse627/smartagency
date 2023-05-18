using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Features.Query.Partners;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Query.Customers
{
    // Customer GetCustomerByIdQuery with Customer response
    public class GetPartnerByIdQuery : IRequest<PartnerResponseDTO>
    {
        public Guid Id { get; private set; }

        public GetPartnerByIdQuery(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class GetPartnerByIdHandler : IRequestHandler<GetPartnerByIdQuery, PartnerResponseDTO>
    {
        private readonly IMediator _mediator;

        public GetPartnerByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<PartnerResponseDTO> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var partners = await _mediator.Send(new GetAllPartnerQuery());
            var selectedPartner = partners.FirstOrDefault(p=>p.Id == request.Id);
            return CustomMapper.Mapper.Map<PartnerResponseDTO>(selectedPartner);
           
        }
    }
}