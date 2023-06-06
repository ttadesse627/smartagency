using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Query
{
    // Customer GetCustomerByIdQuery with Customer response
    public class GetCompanyInformationQuery : IRequest<ServiceResponse<GetCompanyInformationResponseDTO>>
    {
        

    }

    public class GetCompanyInformationHandler : IRequestHandler<GetCompanyInformationQuery,ServiceResponse<GetCompanyInformationResponseDTO>>
    {
        private readonly ICompanyInformationRepository _companyInformationRepository;
        private readonly IFileService _fileService;
        

        public GetCompanyInformationHandler(ICompanyInformationRepository companyInformationRepository, IFileService fileService)
        {
            _companyInformationRepository= companyInformationRepository;
            _fileService=  fileService;
        }
        public async Task<ServiceResponse<GetCompanyInformationResponseDTO>> Handle(GetCompanyInformationQuery request, CancellationToken cancellationToken)
        {
            var companyInformationResponse = new ServiceResponse<GetCompanyInformationResponseDTO>();
            var allCompanyInformation = await _companyInformationRepository.GetAllWithAsync("Address","Witnesses","CountryOperations","CompanySetting","CountryOperations.LookUpCountryOperation","Address.AddressRegion");
            if (allCompanyInformation.Count() > 0)
            {
                var selectedCompanyInformation = allCompanyInformation.First();
                 companyInformationResponse.Data = CustomMapper.Mapper.Map<GetCompanyInformationResponseDTO>(selectedCompanyInformation);
                var companyImageId= selectedCompanyInformation.Id.ToString();
              
            var fileType1="CompanyLetterLogo";
            var fileType2 = "CompanyLetterBackground";

            //string fileName = "Slider" + id.ToString() + ".jpg"; // Replace ".jpg" with the actual file extension
            (byte[], string, string) fileResult1 = _fileService.getFile(companyImageId, fileType1, null);
            (byte[], string, string) fileResult2 = _fileService.getFile(companyImageId, fileType2, null);


        // Convert the byte array of the image content to a Base64 encoded string
        string fileContent1 = Convert.ToBase64String(fileResult1.Item1);
        string fileContent2 = Convert.ToBase64String(fileResult2.Item1);

        // Create an anonymous object with properties "FileName" and "FileContent"
        var response1 = new { FileName = fileResult1.Item2 + fileResult1.Item3, FileContent = fileContent1 };
        var response2 = new { FileName = fileResult2.Item2 + fileResult2.Item3, FileContent = fileContent2 };


            companyInformationResponse.Data.LetterLogo = fileContent1;
            companyInformationResponse.Data.LetterBackGround = fileContent2;

                companyInformationResponse.Success = true;

            }
        return companyInformationResponse;
           
        }
    }
}