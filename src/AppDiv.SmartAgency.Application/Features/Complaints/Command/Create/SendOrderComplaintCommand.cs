using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Features.Complaints.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Command.Create
{
    public record SendOrderComplaintCommand(OrderComplaintRequest Request) : IRequest<GetOrderComplaintsResponseDTO> { }
    public class SendOrderComplaintCommandHandler : IRequestHandler<SendOrderComplaintCommand, GetOrderComplaintsResponseDTO>
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly ISmartAgencyDbContext _context;
        private readonly IMediator _mediator;

        public SendOrderComplaintCommandHandler(IComplaintRepository complaintRepository, IApplicantRepository applicantRepository, ISmartAgencyDbContext context
            , IMediator mediator)
        {
            _complaintRepository = complaintRepository;
            _applicantRepository = applicantRepository;
            _context = context;
            _mediator = mediator;
        }

        public async Task<GetOrderComplaintsResponseDTO> Handle(SendOrderComplaintCommand command, CancellationToken cancellationToken)
        {
            var response = new GetOrderComplaintsResponseDTO();

            var applicant = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == command.Request.ApplicantId);
            var complint = new Complaint
            {
                Message = command.Request.Message,
                ApplicantId = command.Request.ApplicantId,
                CreatedBy = _context.GetCurrentUserId()
            };
            await _complaintRepository.InsertAsync(complint, cancellationToken);
            try
            {
                var success = await _complaintRepository.SaveChangesAsync(cancellationToken);
                if (success)
                {
                    var complaints = await _mediator.Send(new GetOrderComplaintsQuery(applicant.Id));
                    response = complaints;
                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

            return response;
        }
    }

}