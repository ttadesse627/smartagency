

using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroup
{
    public record GetDropDownGroups : IRequest<List<DropDownDto>> { }

    public class GetDropDownGroupsHandler : IRequestHandler<GetDropDownGroups, List<DropDownDto>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetDropDownGroupsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<List<DropDownDto>> Handle(GetDropDownGroups request, CancellationToken cancellationToken)
        {
            var grpdropDowns = await _groupRepository.GetAllWithAsync();
            var dropDownResponse = new List<DropDownDto>();
            foreach (var grItem in grpdropDowns)
            {
                dropDownResponse.Add(new DropDownDto { Key = grItem.Id, Value = grItem.GroupName });
            }
            return dropDownResponse;

        }
    }
}