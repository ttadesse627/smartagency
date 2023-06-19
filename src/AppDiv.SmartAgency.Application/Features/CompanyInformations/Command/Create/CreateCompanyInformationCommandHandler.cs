

using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Create
{
    public class CreateCompanyInformationCommandHandler : IRequestHandler<CreateCompanyInformationCommand, CreateCompanyInformationCommandResponse>
    {
        private readonly ICompanyInformationRepository _companyInformationRepository;
        private readonly IFileService _fileService;
        public CreateCompanyInformationCommandHandler(ICompanyInformationRepository companyInformationRepository, IFileService fileService)
        {
            _companyInformationRepository = companyInformationRepository;
            _fileService = fileService;
        }
        public async Task<CreateCompanyInformationCommandResponse> Handle(CreateCompanyInformationCommand request, CancellationToken cancellationToken)
        {
            var createCompanyInformationCommandResponse = new CreateCompanyInformationCommandResponse();

            var validator = new CreateCompanyInformationCommandValidator(_companyInformationRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createCompanyInformationCommandResponse.Success = false;
                createCompanyInformationCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createCompanyInformationCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createCompanyInformationCommandResponse.Message = createCompanyInformationCommandResponse.ValidationErrors[0];
            }
            if (createCompanyInformationCommandResponse.Success)
            {

                var companyInformationEntity = CustomMapper.Mapper.Map<CompanyInformation>(request.companyInformation);
                var witnesses = new List<Witness>();
                foreach (var witness in request.companyInformation.Witness.Witnesses)
                {
                    var witns = CustomMapper.Mapper.Map<Witness>(witness);
                    witnesses.Add(witns);
                }
                companyInformationEntity.Witnesses = witnesses;
                await _companyInformationRepository.InsertAsync(companyInformationEntity, cancellationToken);
                await _companyInformationRepository.SaveChangesAsync(cancellationToken);


                // save headerlogo
                var file = request.companyInformation.LetterLogo;
                var folderName = Path.Combine("Resources", "CompanyLetterLogo");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = companyInformationEntity.Id.ToString();
                if (!string.IsNullOrEmpty(file))
                {
                    await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);
                }
                var file2 = request.companyInformation.LetterBackGround;
                var folderName2 = Path.Combine("Resources", "CompanyLetterBackground");
                var pathToSave2 = Path.Combine(Directory.GetCurrentDirectory(), folderName2);
                var fileName2 = companyInformationEntity.Id.ToString();
                if (!string.IsNullOrEmpty(file))
                {

                    await _fileService.UploadBase64FileAsync(file2, fileName2, pathToSave2, FileMode.Create);
                }
            }


            return createCompanyInformationCommandResponse;




        }
    }
}
