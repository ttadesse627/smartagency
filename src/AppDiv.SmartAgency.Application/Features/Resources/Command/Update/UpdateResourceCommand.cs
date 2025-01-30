using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Resources.Command.Update
{
    public record UpdateResourceCommand : IRequest<ServiceResponse<int>>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }


    public class EditResourceCommandHandler(IResourceRepository resourceRepository) : IRequestHandler<UpdateResourceCommand, ServiceResponse<Int32>>
    {
        private readonly IResourceRepository _resourceRepository = resourceRepository;

        public async Task<ServiceResponse<int>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            var ResourceEntity = await _resourceRepository.GetAsync(request.Id);

            if (ResourceEntity != null)
            {
                ResourceEntity.Name = request.Name;
                try
                {
                    response.Success = await _resourceRepository.SaveChangesAsync(cancellationToken);
                    if (response.Success)
                    {
                        response.Message = "Update Successful!";
                    }
                }
                catch (Exception exp)
                {
                    response.Errors?.Add(exp.Message);
                    throw new ApplicationException(exp.Message);
                }
            }
            else
            {
                response.Errors?.Add(new NotFoundException($"Resource not found!").Message);
                throw new NotFoundException($"Resource not found!");
            }
            return response;
        }
    }

}