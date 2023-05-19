using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Deposits.Query
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
        private readonly IDepositRepository _depositRepository;

        public GetDepositByIdHandler(IDepositRepository depositRepository)
        {
            _depositRepository= depositRepository;
        }
        public async Task<GetDepositByIdResponseDTO> Handle(GetDepositByIdQuery request, CancellationToken cancellationToken)
        {
            var selectedDeposit = await _depositRepository.GetByIdAsync(request.Id);
            return CustomMapper.Mapper.Map<GetDepositByIdResponseDTO>(selectedDeposit);
           
        }
}

}