using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public record CreateOrderCommand(CreateOrderRequest request) : IRequest<ServiceResponse<Int32>>
{ }
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public CreateOrderCommandHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var createOrderCommandResponse = new ServiceResponse<Int32>();

        var orderEntity = CustomMapper.Mapper.Map<Order>(command.request);
        IEnumerable<Applicant> appOrders = new List<Applicant>();
        if (command.request.EmployeeIds != null && command.request.EmployeeIds.Count > 0)
        {
            appOrders = await _applicantRepository.GetByIdsAsync(command.request.EmployeeIds, appl => appl.IsDeleted == false);
        }
        if (appOrders.Count() > 0)
        {
            orderEntity.Employees = appOrders.ToList();
        }

        orderEntity.Payment?.UpdatePayment(command.request.Payment.CurrentPaidAmount);

        await _orderRepository.InsertAsync(orderEntity, cancellationToken);
        createOrderCommandResponse.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
        if (createOrderCommandResponse.Success)
        {
            createOrderCommandResponse.Data = orderEntity.GetHashCode();
            createOrderCommandResponse.Message = $"Operation Succeeded: {createOrderCommandResponse.Data} entity is created!";
        }

        return createOrderCommandResponse;
    }
}