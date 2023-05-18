using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Deposits
{
    public class GetAllDepositQuery: IRequest<List<DepositResponseDTO>>
    {

    }

    public class GetAllDepositHandler : IRequestHandler<GetAllDepositQuery, List<DepositResponseDTO>>
    {
        private readonly IDepositRepository _depositRepository;

        public GetAllDepositHandler(IDepositRepository depositQueryRepository)
        {
            _depositRepository = depositQueryRepository;
        }
        public async Task<List<DepositResponseDTO>> Handle(GetAllDepositQuery request, CancellationToken cancellationToken)
        {
            var depositList = await _depositRepository.GetAllWithAsync("Applicant");
            var depositResponse = CustomMapper.Mapper.Map<List<DepositResponseDTO>>(depositList);
            return depositResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}