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

    public class GetCompanyInformationHandler : IRequestHandler<GetCompanyInformationQuery, ServiceResponse<GetCompanyInformationResponseDTO>>
    {
        private readonly ICompanyInformationRepository _companyInformationRepository;
        private readonly IFileService _fileService;
        private readonly IUserResolverService _userResolver;


        public GetCompanyInformationHandler(ICompanyInformationRepository companyInformationRepository,
           IFileService fileService, IUserResolverService userResolver)
        {
            _companyInformationRepository = companyInformationRepository;
            _fileService = fileService;
            _userResolver = userResolver;
        }
        public async Task<ServiceResponse<GetCompanyInformationResponseDTO>> Handle(GetCompanyInformationQuery request, CancellationToken cancellationToken)
        {
            var companyInformationResponse = new ServiceResponse<GetCompanyInformationResponseDTO>();
            var allCompanyInformation = await _companyInformationRepository.GetAllWithAsync("Address", "Witnesses", "CountryOperations", "CompanySetting", "CountryOperations.LookUpCountryOperation", "Address.Region");
            if (allCompanyInformation.Count() > 0)
            {
                var selectedCompanyInformation = allCompanyInformation.First();
                // companyInformationResponse.Data = CustomMapper.Mapper.Map<GetCompanyInformationResponseDTO>(selectedCompanyInformation);
                var witnesses = new List<CompanyWitnessRequest>();
                var CountryOperations = new List<CountryOperationResponseDTO>();

                companyInformationResponse.Data = new GetCompanyInformationResponseDTO
                {

                    Id = selectedCompanyInformation.Id,
                    CompanyName = selectedCompanyInformation.CompanyName,
                    CompanyNameAmharic = selectedCompanyInformation.CompanyNameAmharic,
                    CompanyNameArabic = selectedCompanyInformation.CompanyNameArabic,
                    ContractNumber = selectedCompanyInformation.ContractNumber,
                    licenseNumber = selectedCompanyInformation.licenseNumber,
                    AssurancePolicyNumber = selectedCompanyInformation.AssurancePolicyNumber,
                    GeneralManager = selectedCompanyInformation.AssurancePolicyNumber,
                    GeneralManagerAmharic = selectedCompanyInformation.GeneralManagerAmharic,
                    ViceManager = selectedCompanyInformation.ViceManager,
                    ViceManagerAmharic = selectedCompanyInformation.ViceManagerAmharic,
                    CountriesOperation = selectedCompanyInformation.CountriesOperation,
                    Address = new CompanyAddressResponseDTO
                    {
                        Region = new Contracts.DTOs.LookUpDTOs.LookUpItemResponseDTO
                        {
                            Id = selectedCompanyInformation.Address.RegionId,
                            Value = selectedCompanyInformation.Address.Region.Value
                        },
                        SubCity = selectedCompanyInformation.Address.SubCity,
                        Zone = selectedCompanyInformation.Address.Zone,
                        Woreda = selectedCompanyInformation.Address.Woreda,
                        Adress = selectedCompanyInformation.Address.Adress,
                        PostCode = selectedCompanyInformation.Address.PostCode,
                        PhoneNumber = selectedCompanyInformation.Address.PhoneNumber,
                        HouseNumber = selectedCompanyInformation.Address.HouseNumber,
                        OfficePhone = selectedCompanyInformation.Address.OfficePhone,
                        Mobile = selectedCompanyInformation.Address.Mobile,
                        AlternativePhone = selectedCompanyInformation.Address.AlternativePhone,
                        Fax = selectedCompanyInformation.Address.Fax,
                        Email = selectedCompanyInformation.Address.Email
                    },

                    Witness = new CompanyWitnssRequest
                    {
                        Witnesses = selectedCompanyInformation.Witnesses.Select(wit => new CompanyWitnessRequest
                        {
                            FullName = wit.FullName,
                            Address = wit.Address,
                            PhoneNumber = wit.PhoneNumber,
                        }).ToList()
                    },

                    CountryOperations = selectedCompanyInformation.CountryOperations.Select(co => new CountryOperationResponseDTO
                    {
                        Country = new Contracts.DTOs.LookUpDTOs.LookUpItemResponseDTO
                        {
                            Id = co.CountryId,
                            Value = co.LookUpCountryOperation.Value
                        },
                        LicenseNumber = co.LicenseNumber,
                        VisaExpiryDays = co.VisaExpiryDays
                    }).ToList(),

                    CompanySetting = new CompanySettingRequest
                    {
                        FileNumberStartFrom = selectedCompanyInformation.CompanySetting.FileNumberStartFrom,
                        PrintedDocumentSubmitDays = selectedCompanyInformation.CompanySetting.PrintedDocumentSubmitDays,
                        AmountOfDeposit = selectedCompanyInformation.CompanySetting.AmountOfDeposit,
                        AuthorizedPerson = selectedCompanyInformation.CompanySetting.AuthorizedPerson,
                        PenalityInterval = selectedCompanyInformation.CompanySetting.PenalityInterval,
                        PenalityAmount = selectedCompanyInformation.CompanySetting.PenalityAmount,
                    }
                };

                var companyImageId = selectedCompanyInformation.Id.ToString();

                var fileType1 = "CompanyLetterLogo";
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

                Console.WriteLine("//////////////////////");
                Console.WriteLine("//////////////////////");
                Console.WriteLine("//////////////////////");
                Console.WriteLine("//////////////////////");
                Console.WriteLine(_userResolver.GetUserId());
            }
            return companyInformationResponse;
        }
    }
}