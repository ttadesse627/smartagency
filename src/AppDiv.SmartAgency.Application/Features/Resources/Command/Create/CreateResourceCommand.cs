using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Resources.Command.Create
{
    public record CreateResourceCommand(string ResourceName) : IRequest<ServiceResponse<int>> { }
    public class CreateResourceCommandHandler(IResourceRepository resourceRepository) : IRequestHandler<CreateResourceCommand, ServiceResponse<int>>
    {
        private readonly IResourceRepository _resourceRepository = resourceRepository;

        public async Task<ServiceResponse<int>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {

            var createResourceCommandResponse = new ServiceResponse<int>();
            var validator = new CreateResourceCommandValidator(_resourceRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createResourceCommandResponse.Errors = [];
                foreach (var error in validationResult.Errors)
                {
                    createResourceCommandResponse.Errors.Add(error.ErrorMessage);
                }
                createResourceCommandResponse.Message = createResourceCommandResponse.Errors[0];
            }
            if (validationResult.IsValid)
            {
                var Resource = new Resource(){Name = request.ResourceName};

                await _resourceRepository.InsertAsync(Resource, cancellationToken);
                createResourceCommandResponse.Success = await _resourceRepository.SaveChangesAsync(cancellationToken);

                if (createResourceCommandResponse.Success)
                {
                    createResourceCommandResponse.Message = "Successfully inserted!";
                }
                else createResourceCommandResponse.Message = "Failed to save your data!";
            }
            else
            {
                createResourceCommandResponse.Success = false;
                createResourceCommandResponse.Errors?.AddRange((IEnumerable<string>)validationResult.Errors);
            }


            return createResourceCommandResponse;
        }
    }
}