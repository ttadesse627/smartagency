

using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Attachments;
    public class GetAttachmentByCodeHandler : IRequestHandler<GetAttachmentByCode, Attachment>
    {
        private readonly IMediator _mediator;

        public GetAttachmentByCodeHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Attachment> Handle(GetAttachmentByCode request)
        {
            var attachments = await _mediator.Send(new GetAllAttachments());
            var selectedAttachment = attachments.FirstOrDefault(attachment => attachment.Code == request.Code);
            return CustomMapper.Mapper.Map<Attachment>(selectedAttachment);
            // return selectedCustomer;
        }

    public Task<Attachment> Handle(GetAttachmentByCode request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}