using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;

namespace AppDiv.SmartAgency.Application.Features.Query.Attachments
{
    public record GetAttachmentQuery(Guid id) : IRequest<AttachmentResponseDTO> { }

    public class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, AttachmentResponseDTO>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ISmartAgencyDbContext _dbContext;

        public GetAttachmentQueryHandler(IAttachmentRepository attachmentRepository, ISmartAgencyDbContext dbContext)
        {
            _attachmentRepository = attachmentRepository;
            _dbContext = dbContext;
        }
        public async Task<AttachmentResponseDTO> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _attachmentRepository.GetAsync(request.id);
            var attachmentResponse = CustomMapper.Mapper.Map<AttachmentResponseDTO>(attachment);
            return attachmentResponse;
        }
    }
}