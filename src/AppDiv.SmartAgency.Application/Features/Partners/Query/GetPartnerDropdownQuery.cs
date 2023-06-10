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
    private readonly ISmartAgencyDbContext _dbContext;

    public GetPartnerDropdownQueryHandler(IPartnerRepository partnerQueryRepository, ISmartAgencyDbContext dbContext)
    {
        _partnerRepository = partnerQueryRepository;
        _dbContext = dbContext;
    }
    public async Task<PartnerDropdownContainerDTO> Handle(GetPartnerDropdownQuery request, CancellationToken cancellationToken)
    {
        var partnerResponse = new PartnerDropdownContainerDTO();
        var partnerList = await _partnerRepository.GetAllWithAsync("Orders");
        if (partnerList.Count() > 0)
        {
            var partners = new List<GetPartnerDropDownDTO>();
            foreach (var partner in partnerList)
            {
                var words = partner.PartnerName.Split(' ');
                var abbrName = new StringBuilder();
                foreach (var word in words)
                {
                    abbrName.Append(word.First());
                }
                var partResponse = new GetPartnerDropDownDTO
                {
                    Id = partner.Id,
                    PartnerName = partner.PartnerName,
                    OrderNumber = abbrName.ToString() + " 00" + partner.Orders.Count + 1
                };
                partners.Add(partResponse);
            }
            partnerResponse.partners = partners;
        }
        return partnerResponse;
    }
}