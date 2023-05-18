using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Applicants.OnlineApplicants
{
    public class GetAllOnlineApplicantQuery: IRequest<List<OnlineApplicantResponseDTO>>
    {

    }

    public class GetAllOnlineApplicantHandler : IRequestHandler<GetAllOnlineApplicantQuery, List<OnlineApplicantResponseDTO>>
    {
        private readonly IOnlineApplicantRepository _onlineApplicantRepository;

        public GetAllOnlineApplicantHandler(IOnlineApplicantRepository onlineApplicantQueryRepository)
        {
            _onlineApplicantRepository = onlineApplicantQueryRepository;
        }
        public async Task<List<OnlineApplicantResponseDTO>> Handle(GetAllOnlineApplicantQuery request, CancellationToken cancellationToken)
        {
            var onlineApplicantList = await _onlineApplicantRepository.GetAllWithAsync("MaritalStatus","Experience","DesiredCountry");
            var onlineApplicantResponse = CustomMapper.Mapper.Map<List<OnlineApplicantResponseDTO>>(onlineApplicantList);
            return onlineApplicantResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}