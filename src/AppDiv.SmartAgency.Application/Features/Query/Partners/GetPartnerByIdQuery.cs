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
    public class GetPartnerByIdQuery : IRequest<Partner>
    {
        public string Id { get; private set; }

        public GetPartnerByIdQuery(string Id)
        {
            this.Id = Id;
        }

    }

    public class GetPartnerByIdHandler : IRequestHandler<GetPartnerByIdQuery, Partner>
    {
        private readonly IMediator _mediator;

        public GetPartnerByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Partner> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var partners = await _mediator.Send(new GetAllPartnerQuery());
            var selectedPartner = partners.FirstOrDefault(x => x.Id == request.Id);
            return CustomMapper.Mapper.Map<Partner>(selectedPartner);
            // return selectedCustomer;
        }
    }
}