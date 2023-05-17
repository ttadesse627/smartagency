using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Applicants.OnlineApplicants
{
    public class GetOnlineApplicantByIdQuery: IRequest<OnlineApplicantResponseDTO>
    {
         public Guid  Id { get; private set; }

        public GetOnlineApplicantByIdQuery(Guid id)
        {
            Id = id;
        }

    }

    public class GetOnlineApplicantByIdHandler : IRequestHandler<GetOnlineApplicantByIdQuery, OnlineApplicantResponseDTO>
    {
         private readonly IOnlineApplicantRepository _onlineApplicantRepository;
        public GetOnlineApplicantByIdHandler(IOnlineApplicantRepository onlineApplicantRepository)
        {
           _onlineApplicantRepository = onlineApplicantRepository;
        }
        public async Task<OnlineApplicantResponseDTO> Handle(GetOnlineApplicantByIdQuery request, CancellationToken cancellationToken)
        {
                    //  var onlineApplicants = await _mediator.Send(new GetAllOnlineApplicantQuery());

                //var id = new object[] { request.id };

            var explicitLoadedProperties = new Dictionary<string, NavigationPropertyType>
            {
                { "MaritalStatus", NavigationPropertyType.REFERENCE },
                { "DesiredCountry", NavigationPropertyType.REFERENCE },
                { "Experience", NavigationPropertyType.REFERENCE }
            };

                var selectedOnlineApplicant = await _onlineApplicantRepository.GetWithAsync(request.Id, explicitLoadedProperties);
                return CustomMapper.Mapper.Map<OnlineApplicantResponseDTO>(selectedOnlineApplicant);
           
        }
    }
}