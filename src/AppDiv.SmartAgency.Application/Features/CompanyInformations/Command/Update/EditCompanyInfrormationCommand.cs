
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Update
{
    public record EditCompanyInformationCommand(CreateCompanyInformationRequest companyInfromationRequest) : IRequest<GetCompanyInformationResponseDTO>
    {
        
       
    }



   

    public class EditCompanyInformationCommandHandler : IRequestHandler<EditCompanyInformationCommand, GetCompanyInformationResponseDTO>
    {
   
        private readonly ICompanyInformationRepository _companyInformationRepository;
     
        public EditCompanyInformationCommandHandler(ICompanyInformationRepository companyInformationRepository)
        {
            _companyInformationRepository = companyInformationRepository;
        }
            
      
        public async Task<GetCompanyInformationResponseDTO> Handle(EditCompanyInformationCommand request,  CancellationToken cancellationToken)
        {
            var companyInformationResponse = new GetCompanyInformationResponseDTO();
          
            var companyInformationEntity = CustomMapper.Mapper.Map<CompanyInformation>(request.companyInfromationRequest);
            var companyInformationExistence= await _companyInformationRepository.GetByIdAsync(request.companyInfromationRequest.Id);

            if (companyInformationExistence!=null){

                try
            {
                var res =    await _companyInformationRepository.UpdateAsync(companyInformationEntity);
                

                if(res>=1) {

                    //var modifiedCompanyInformation = await _companyInformationRepository.GetByIdAsync(request.companyInfromationRequest.Id);
                    var modifiedCompanyInformation = await _companyInformationRepository.GetWithPredicateAsync(comInfo => comInfo.Id == request.companyInfromationRequest.Id,"Address","Witnesses","CountryOperations","CompanySetting","CountryOperations.LookUpCountryOperation","Address.AddressRegion");
                    companyInformationResponse = CustomMapper.Mapper.Map<GetCompanyInformationResponseDTO>(modifiedCompanyInformation);
                  

                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
           
            }else{

                try{

                    await _companyInformationRepository.InsertAsync(companyInformationEntity, cancellationToken);
                    _companyInformationRepository.SaveChangesAsync(cancellationToken);

                    var modifiedCompanyInformation = await _companyInformationRepository.GetWithPredicateAsync(comInfo => comInfo.Id == request.companyInfromationRequest.Id,"Address","Witnesses","CountryOperations","CompanySetting","CountryOperations.LookUpCountryOperation","Address.AddressRegion");
                    companyInformationResponse = CustomMapper.Mapper.Map<GetCompanyInformationResponseDTO>(modifiedCompanyInformation);
                   
                }
                catch(Exception exp){
                    throw new ApplicationException(exp.Message);
                }

            }

           
          
          //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
           // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

            return companyInformationResponse;
        }

        
    }

}