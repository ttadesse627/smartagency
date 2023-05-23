
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Create
{
    public class CreateCompanyInformationCommandHandler : IRequestHandler<CreateCompanyInformationCommand, CreateCompanyInformationCommandResponse>
{
        private readonly ICompanyInformationRepository _companyInformationRepository;
        public CreateCompanyInformationCommandHandler(ICompanyInformationRepository companyInformationRepository)
        {
            _companyInformationRepository = companyInformationRepository;
        }
        public async Task<CreateCompanyInformationCommandResponse> Handle(CreateCompanyInformationCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

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
            await _companyInformationRepository.InsertAsync(companyInformationEntity, cancellationToken);
            var result = await _companyInformationRepository.SaveChangesAsync(cancellationToken);      
            }
            return createCompanyInformationCommandResponse;
        }
}
}