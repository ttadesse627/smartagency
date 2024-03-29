

using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroup
{
    public record GetDropDownGroups : IRequest<UserGroupResponseDTO> { }

    public class GetDropDownGroupsHandler : IRequestHandler<GetDropDownGroups, UserGroupResponseDTO>
    {
        private readonly IGroupRepository _groupRepository;

        public GetDropDownGroupsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<UserGroupResponseDTO> Handle(GetDropDownGroups request, CancellationToken cancellationToken)
        {
            var grpdropDowns = await _groupRepository.GetAllWithAsync();
            var userGroups = new UserGroupResponseDTO();
            var dropDownResponse = new List<DropDownDto>();
            foreach (var grItem in grpdropDowns)
            {
                dropDownResponse.Add(new DropDownDto { Key = grItem.Id, Value = grItem.GroupName });
            }

            userGroups.UserGroups = dropDownResponse;
            return userGroups;

        }
    }
}