using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Command.Create
{
    public record SendOrderComplaintCommand(OrderComplaintRequest Request) : IRequest<List<GetComplaintResponseDTO>> { }
    public class SendOrderComplaintCommandHandler : IRequestHandler<SendOrderComplaintCommand, List<GetComplaintResponseDTO>>
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly ISmartAgencyDbContext _context;

        public SendOrderComplaintCommandHandler(IComplaintRepository complaintRepository, IApplicantRepository applicantRepository, ISmartAgencyDbContext context)
        {
            _complaintRepository = complaintRepository;
            _applicantRepository = applicantRepository;
            _context = context;
        }

        public async Task<List<GetComplaintResponseDTO>> Handle(SendOrderComplaintCommand request, CancellationToken cancellationToken)
        {
            var response = new List<GetComplaintResponseDTO>();

            var applicant = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == request.Request.ApplicantId);
            var complint = new Complaint
            {
                Message = request.Request.Message,
                ApplicantId = request.Request.ApplicantId,
                CreatedBy = _context.GetCurrentUserId()
            };
            await _complaintRepository.InsertAsync(complint, cancellationToken);
            try
            {
                var success = await _complaintRepository.SaveChangesAsync(cancellationToken);
                if (success)
                {
                    var complaints = await _complaintRepository.GetAllWithPredicateAsync(comp => comp.ApplicantId == request.Request.ApplicantId, "User");
                    if (complaints.Count > 0 || complaints != null)
                    {

                        foreach (var complaint in complaints)
                        {
                            var comResponse = new GetComplaintResponseDTO
                            {
                                Message = complaint.Message,
                                SenderName = complaint.User.FullName,
                                Date = complaint.CreatedAt
                            };
                            response.Add(comResponse);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

            return response;
        }
    }

}