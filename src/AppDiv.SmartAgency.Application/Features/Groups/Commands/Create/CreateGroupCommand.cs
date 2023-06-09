
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Groups;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
namespace AppDiv.SmartAgency.Application.Features.Groups.Commands.Create
{
    public record CreateGroupCommand(AddGroupRequest group) : IRequest<ServiceResponse<int>> { }
    public class CreateGroupCommandHAndler : IRequestHandler<CreateGroupCommand, ServiceResponse<int>>
    {
        private readonly IGroupRepository _groupRepository;
        public CreateGroupCommandHAndler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<ServiceResponse<int>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();

            // var validator = new CreateGroupComadValidetor(_groupRepository);
            // var validationResult = await validator.ValidateAsync(request, cancellationToken);

            // //Check and log validation errors
            // if (validationResult.Errors.Count > 0)
            // {
            //     response.Success = false;
            //     response.Errors = new List<string>();
            //     foreach (var error in validationResult.Errors)
            //         response.Errors.Add(error.ErrorMessage);
            //     response.Message = response.Errors[0];
            // }  //can use this instead of automapper
            var group = new UserGroup
            {
                Id = Guid.NewGuid(),
                GroupName = request.group.GroupName,
                Description = request.group.Description,
                Roles = request.group.Roles
            };

            try
            {
                await _groupRepository.InsertAsync(group, cancellationToken);
                var result = await _groupRepository.SaveChangesAsync(cancellationToken);
                if (result)
                {
                    response.Success = true;
                    response.Message = "Created Successfully!";
                }
            }
            catch (Exception ex)
            {
                response.Errors?.Add(ex.Message);
                throw new BadRequestException(ex.Message);
            }
            return response;
        }
    }
}