using AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs;
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

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Query
{
    // Customer GetCustomerByIdQuery with Customer response
    public class GetCompanyInformationQuery : IRequest<List<GetCompanyInformationResponseDTO>>
    {
        

    }

    public class GetCompanyInformationHandler : IRequestHandler<GetCompanyInformationQuery,List<GetCompanyInformationResponseDTO>>
    {
        private readonly ICompanyInformationRepository _companyInformationRepository;
        

        public GetCompanyInformationHandler(ICompanyInformationRepository companyInformationRepository)
        {
            _companyInformationRepository= companyInformationRepository;
        }
        public async Task<List<GetCompanyInformationResponseDTO>> Handle(GetCompanyInformationQuery request, CancellationToken cancellationToken)
        {
            //var partners = await _mediator.Send(new GetAllPartnerQuery());
            var selectedCompanyInformation = await _companyInformationRepository.GetAllWithAsync("Address","Witnesses","CountryOperations","CompanySetting","CountryOperations.LookUpCountryOperation","Address.AddressRegion");

            return CustomMapper.Mapper.Map<List<GetCompanyInformationResponseDTO>>(selectedCompanyInformation);
           
        }
    }
}