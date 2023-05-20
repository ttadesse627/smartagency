using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Partners.Query
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

    public class GetPartnerByIdHandler : IRequestHandler<GetPartnerByIdQuery,PartnerResponseDTO>
    {
        private readonly IPartnerRepository _partnerRepository;
        

        public GetPartnerByIdHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository= partnerRepository;
        }
        public async Task<PartnerResponseDTO> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            //var partners = await _mediator.Send(new GetAllPartnerQuery());
            var selectedPartner = await _partnerRepository.GetByIdAsync(request.Id);
            return CustomMapper.Mapper.Map<PartnerResponseDTO>(selectedPartner);
           
        }
    }
}