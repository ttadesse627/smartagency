using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using System.Text;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;

namespace AppDiv.SmartAgency.Application.Features.Partners.Query;
public class GetPartnerDropdownQuery : IRequest<PartnerDropdownContainerDTO> { }

public class GetPartnerDropdownQueryHandler : IRequestHandler<GetPartnerDropdownQuery, PartnerDropdownContainerDTO>
{
    private readonly IPartnerRepository _partnerRepository;

    public GetPartnerDropdownQueryHandler(IPartnerRepository partnerQueryRepository)
    {
        _partnerRepository = partnerQueryRepository;
    }
    public async Task<PartnerDropdownContainerDTO> Handle(GetPartnerDropdownQuery request, CancellationToken cancellationToken)
    {
        var partnerResponse = new PartnerDropdownContainerDTO();
        var partnerList = await _partnerRepository.GetAllWithAsync("Orders");

        if (partnerList.Any())
        {
            var partners = new List<GetPartnerDropDownDTO>();
            foreach (var partner in partnerList)
            {
                var words = partner.PartnerName!.Split(' ');
                var abbrName = new StringBuilder();
                foreach (var word in words)
                {
                    abbrName.Append(word.First());
                }
                var orderCount = partner.Orders?.Count + 1;
                var partResponse = new GetPartnerDropDownDTO
                {
                    Id = partner.Id,
                    PartnerName = partner.PartnerName,
                    OrderNumber = abbrName.ToString() + " 00" + orderCount
                };
                partners.Add(partResponse);
            }
            partnerResponse.partners = partners;
        }
        return partnerResponse;
    }
}