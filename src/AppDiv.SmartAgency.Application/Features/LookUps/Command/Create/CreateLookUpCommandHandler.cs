using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.LookUps;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Create
{
    public record CreateLookUpCommand(CreateLookUpRequest LookUp) : IRequest<ServiceResponse<int>> { }
    public class CreateLookUpCommandHandler(ILookUpRepository lookUpRepository) : IRequestHandler<CreateLookUpCommand, ServiceResponse<int>>
    {
        private readonly ILookUpRepository _lookUpRepository = lookUpRepository;

        public async Task<ServiceResponse<int>> Handle(CreateLookUpCommand request, CancellationToken cancellationToken)
        {

            var createLookUpCommandResponse = new ServiceResponse<int>();
            var validator = new CreateLookUpCommandValidator(_lookUpRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createLookUpCommandResponse.Errors = [];
                foreach (var error in validationResult.Errors)
                {
                    createLookUpCommandResponse.Errors.Add(error.ErrorMessage);
                }
                createLookUpCommandResponse.Message = createLookUpCommandResponse.Errors[0];
            }
            if (validationResult.IsValid)
            {
                var lookUp = new LookUp()
                {
                    Category = request.LookUp.Category!,
                    Value = request.LookUp.Value!
                };

                await _lookUpRepository.InsertAsync(lookUp, cancellationToken);
                createLookUpCommandResponse.Success = await _lookUpRepository.SaveChangesAsync(cancellationToken);

                if (createLookUpCommandResponse.Success)
                {
                    createLookUpCommandResponse.Message = "Successfully inserted!";
                }
                else createLookUpCommandResponse.Message = "Failed to save your data!";
            }
            else
            {
                createLookUpCommandResponse.Success = false;
                createLookUpCommandResponse.Errors?.AddRange((IEnumerable<string>)validationResult.Errors);
            }


            return createLookUpCommandResponse;
        }
    }
}