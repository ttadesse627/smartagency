
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Update;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Update
{
    public class EditCompanyInformationCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }

        public string CompanyNameAmharic { get; set; }

        public string CompanyNameArabic { get; set; }

        public string? ContractNumber { get; set; }

        public string? licenseNumber { get; set; }
        public string AssurancePolicyNumber { get; set; }

        public string GeneralManager { get; set; }
        public string? GeneralManagerAmharic { get; set; }
        public string? ViceManager { get; set; }
        public string? ViceManagerAmharic { get; set; }
        public string? CountriesOperation { get; set; }
        public string? LetterLogo { get; set; }
        public string? LetterBackGround { get; set; }
        public EditCompanyAddressRequest? Address { get; set; }

        public EditCompanyWitnssRequest? Witness { get; set; }
        public ICollection<EditCountryOperationRequest>? CountryOperations { get; set; }
        public EditCompanySettingRequest? CompanySetting { get; set; }

    }
}

public class EditCompanyInformationCommandHandler : IRequestHandler<EditCompanyInformationCommand, string>
{
    private readonly IFileService _fileService;
    private readonly ICompanyInformationRepository _companyInformationRepository;
    public EditCompanyInformationCommandHandler(ICompanyInformationRepository companyInformationRepository, IFileService fileService)
    {
        _companyInformationRepository = companyInformationRepository;
        _fileService = fileService;
    }
    public async Task<string> Handle(EditCompanyInformationCommand request, CancellationToken cancellationToken)
    {
        //var fetchedcompanyInformationEntity =await _companyInformationRepository.update(request.Id);
        var companyInformationEntity = CustomMapper.Mapper.Map<CompanyInformation>(request);

        var witnesses = new List<Witness>();
        foreach (var witness in request.Witness.Witnesses)
        {
            var witns = CustomMapper.Mapper.Map<Witness>(witness);
            witnesses.Add(witns);
        }
        companyInformationEntity.Witnesses = witnesses;



        try
        {
            // fetchedcompanyInformationEntity = companyInformationEntity;
            // var res= await _companyInformationRepository.SaveChangesAsync(cancellationToken);

            var res = await _companyInformationRepository.UpdateAsync(companyInformationEntity);
            await _companyInformationRepository.SaveChangesAsync(cancellationToken);

            if (res >= 1)
            {

                // save headerlogo;
                var file = request.LetterLogo;
                var folderName = Path.Combine("Resources", "CompanyLetterLogo");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = request.Id.ToString();
                if (!string.IsNullOrEmpty(file))
                {

                    await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);

                    var file2 = request.LetterLogo;
                    var folderName2 = Path.Combine("Resources", "CompanyLetterBackground");
                    var pathToSave2 = Path.Combine(Directory.GetCurrentDirectory(), folderName2);
                    var fileName2 = request.Id.ToString();
                    if (!string.IsNullOrEmpty(file))
                    {

                        await _fileService.UploadBase64FileAsync(file2, fileName2, pathToSave2, FileMode.Create);

                    }
                }
            }
        }

        catch (Exception exp)
        {
            throw new ApplicationException(exp.Message);
        }


        //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
        // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

        return "sucessfully updated";
    }
}












