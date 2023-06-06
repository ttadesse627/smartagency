using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Create
{
    public class CreateLookUpCommandHandler : IRequestHandler<CreateLookUpCommand, ServiceResponse<int>>
    {
        private readonly ILookUpRepository _lookUpRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CreateLookUpCommandHandler(ILookUpRepository lookUpRepository, ICategoryRepository categoryRepository)
        {
            _lookUpRepository = lookUpRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ServiceResponse<int>> Handle(CreateLookUpCommand request, CancellationToken cancellationToken)
        {

            var createLookUpCommandResponse = new ServiceResponse<int>();
            var validator = new CreateLookUpCommandValidator(_lookUpRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createLookUpCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createLookUpCommandResponse.Errors.Add(error.ErrorMessage);
                createLookUpCommandResponse.Message = createLookUpCommandResponse.Errors[0];
            }
            if (validationResult.IsValid)
            {
                var lookUp = new LookUp()
                {
                    Category = request.lookUp.Category,
                    Value = request.lookUp.Value
                };

                await _lookUpRepository.InsertAsync(lookUp, cancellationToken);
                createLookUpCommandResponse.Success = await _lookUpRepository.SaveChangesAsync(cancellationToken);

                if (createLookUpCommandResponse.Success)
                {
                    createLookUpCommandResponse.Message = "Successfully inserted!";
                }
                else createLookUpCommandResponse.Message = "Failed to save your data!";
            }
            return createLookUpCommandResponse;
        }
    }
}