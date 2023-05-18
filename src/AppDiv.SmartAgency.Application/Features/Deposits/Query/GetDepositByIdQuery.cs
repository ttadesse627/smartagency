using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Deposits
{
    public class GetDepositByIdQuery: IRequest<GetDepositByIdResponseDTO>
    {
        public Guid Id { get; private set; }

        public GetDepositByIdQuery(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class GetDepositByIdHandler : IRequestHandler<GetDepositByIdQuery, GetDepositByIdResponseDTO>
    {
        private readonly IMediator _mediator;

        public GetDepositByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<GetDepositByIdResponseDTO> Handle(GetDepositByIdQuery request, CancellationToken cancellationToken)
        {
            var deposits = await _mediator.Send(new GetAllDepositQuery());
            var selectedDeposit = deposits.FirstOrDefault(d=>d.Id == request.Id);
            return CustomMapper.Mapper.Map<GetDepositByIdResponseDTO>(selectedDeposit);
           
        }
}

}