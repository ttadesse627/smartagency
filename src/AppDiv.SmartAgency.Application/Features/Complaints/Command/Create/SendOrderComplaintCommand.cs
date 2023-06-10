using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Command.Create
{
    public record SendOrderComplaintCommand(OrderComplaintRequest Request) : IRequest<List<GetComplaintResponseDTO>> { }
    public class SendOrderComplaintCommandHandler : IRequestHandler<SendOrderComplaintCommand, List<GetComplaintResponseDTO>>
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISmartAgencyDbContext _context;

        public SendOrderComplaintCommandHandler(IComplaintRepository complaintRepository, IOrderRepository orderRepository, ISmartAgencyDbContext context)
        {
            _complaintRepository = complaintRepository;
            _orderRepository = orderRepository;
            _context = context;
        }

        public async Task<List<GetComplaintResponseDTO>> Handle(SendOrderComplaintCommand request, CancellationToken cancellationToken)
        {
            var response = new List<GetComplaintResponseDTO>();
            var complint = new Complaint
            {
                Message = request.Request.Message,
                OrderId = request.Request.OrderId,
                UserId = _context.GetCurrentUserId()
            };
            await _complaintRepository.InsertAsync(complint, cancellationToken);

            try
            {
                var success = await _complaintRepository.SaveChangesAsync(cancellationToken);
                if (success)
                {
                    var complaints = await _complaintRepository.GetAllWithPredicateAsync(comp => comp.OrderId == request.Request.OrderId, "User");
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