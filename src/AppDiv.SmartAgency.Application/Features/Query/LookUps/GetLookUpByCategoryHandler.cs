

using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetLookUpByCategoryHandler : IRequestHandler<GetLookUpByCategory, LookUp>
    {
        private readonly IMediator _mediator;

        public GetLookUpByCategoryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<LookUp> Handle(GetLookUpByCategory request)
        {
            var lookUps = await _mediator.Send(new GetAllLookUps());
            var selectedLookUp = lookUps.FirstOrDefault(lookUp => lookUp.Category == request.Category);
            return CustomMapper.Mapper.Map<LookUp>(selectedLookUp);
            // return selectedCustomer;
        }

    public Task<LookUp> Handle(GetLookUpByCategory request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
}