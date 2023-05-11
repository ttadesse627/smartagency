
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Update.Orders;
public record EditOrderCommand(EditOrderRequest editOrderRequest) : IRequest<ServiceResponse<Int32>>
{
}

public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    public EditOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        var editOrderRequest = request.editOrderRequest;
        var response = new ServiceResponse<int>();
        var serviceResponse = await _orderRepository.GetOrderAsync(editOrderRequest.Id);

        var orderToBeEdited = serviceResponse.Data;
        if (orderToBeEdited is not null)
        {
            orderToBeEdited = CustomMapper.Mapper.Map<Order>(request.editOrderRequest);
            // var ordReq = request.editOrderRequest;
            // orderToBeEdited.OrderNumber = ordReq.OrderNumber;
            // orderToBeEdited.VisaNumber = ordReq.VisaNumber;
            // orderToBeEdited.ContractDuration = ordReq.ContractDuration;
            // orderToBeEdited.VisaDate = ordReq.VisaDate;
            // orderToBeEdited.ContractNumber = ordReq.ContractNumber;
            // orderToBeEdited.ElectronicVisaNumber = ordReq.ElectronicVisaNumber;
            // orderToBeEdited.ElectronicVisaDate = ordReq.ElectronicVisaDate;
            // orderToBeEdited.PortOfArrivalId = ordReq.PortOfArrivalId;
            // orderToBeEdited.PriorityId = ordReq.PriorityId;
            // orderToBeEdited.VisaTypeId = ordReq.VisaTypeId;

            // orderToBeEdited.VisaFile!.FileCollectionAttachmentId = ordReq.VisaFile!.FileCollectionAttachmentId;
            // orderToBeEdited.VisaFile.FilePath = ordReq.VisaFile.FilePath;

            // orderToBeEdited.OrderCriteria!.Age = ordReq.OrderCriteria!.Age;
            // orderToBeEdited.OrderCriteria.NationalityId = ordReq.OrderCriteria.NationalityId;
            // orderToBeEdited.OrderCriteria.OrderCriteriaJobTitleId = ordReq.OrderCriteria.OrderCriteriaJobTitleId;
            // orderToBeEdited.OrderCriteria.SalaryId = ordReq.OrderCriteria.SalaryId;
            // orderToBeEdited.OrderCriteria.ReligionId = ordReq.OrderCriteria.ReligionId;
            // orderToBeEdited.OrderCriteria.ExperienceId = ordReq.OrderCriteria.ExperienceId;
            // orderToBeEdited.OrderCriteria.LanguageId = ordReq.OrderCriteria.LanguageId;
            // orderToBeEdited.OrderCriteria.Remark = ordReq.OrderCriteria.Remark;

            // orderToBeEdited.OrderSponsor!.IdNumber = ordReq.OrderSponsor!.IdNumber!;
            // orderToBeEdited.OrderSponsor.FullName = ordReq.OrderSponsor!.FullName!;
            // orderToBeEdited.OrderSponsor.FullNameAmharic = ordReq.OrderSponsor.FullNameAmharic;
            // orderToBeEdited.OrderSponsor.OtherName = ordReq.OrderSponsor.OtherName;
            // orderToBeEdited.OrderSponsor.ResidentialTitle = ordReq.OrderSponsor.ResidentialTitle;
            // orderToBeEdited.OrderSponsor.NumberOfFamily = ordReq.OrderSponsor.NumberOfFamily;
            // orderToBeEdited.OrderSponsor.SponsorIDFile!.FileCollectionAttachmentId = ordReq.OrderSponsor.SponsorIDFile!.FileCollectionAttachmentId;
            // orderToBeEdited.OrderSponsor.SponsorIDFile.FilePath = ordReq.OrderSponsor.SponsorIDFile.FilePath;
            // orderToBeEdited.OrderSponsor.SponsorAddress!.AddressCountryId = ordReq.OrderSponsor.SponsorAddress!.AddressCountryId;
            // orderToBeEdited.OrderSponsor.SponsorAddress.Region = ordReq.OrderSponsor.SponsorAddress.Region;
            // orderToBeEdited.OrderSponsor.SponsorAddress.Zone = ordReq.OrderSponsor.SponsorAddress.Zone;
            // orderToBeEdited.OrderSponsor.SponsorAddress.Woreda = ordReq.OrderSponsor.SponsorAddress.Woreda;
            // orderToBeEdited.OrderSponsor.SponsorAddress.Kebele = ordReq.OrderSponsor.SponsorAddress.Kebele;
            // orderToBeEdited.OrderSponsor.SponsorAddress.PhoneNumber = ordReq.OrderSponsor.SponsorAddress.PhoneNumber;
            // orderToBeEdited.OrderSponsor.SponsorAddress.Email = ordReq.OrderSponsor.SponsorAddress.Email;
            // orderToBeEdited.OrderSponsor.SponsorAddress.Website = ordReq.OrderSponsor.SponsorAddress.Website;
            // orderToBeEdited.OrderSponsor.NumberOfFamily = ordReq.OrderSponsor.NumberOfFamily;

            // orderToBeEdited.OrderPayment!.TotalAmount = ordReq.OrderPayment!.TotalAmount;
            // orderToBeEdited.OrderPayment.PaidAmount = ordReq.OrderPayment.PaidAmount;
            // orderToBeEdited.OrderPayment.CurrentPaidAmount = ordReq.OrderPayment.CurrentPaidAmount;



            // var returnedMessage = _orderRepository.UpdateOrder(orderToBeEdited);
            response.Message = _orderRepository.UpdateOrder(orderToBeEdited);
            response = await _orderRepository.SaveDbUpdateAsync();
            if (response.Data >= 1)
            {
                response.Message = $"Successfully updated the order with an id {editOrderRequest.Id}";
                response.Success = true;
            }
        }
        else if (orderToBeEdited is null)
        {
            response.Message = $"An order with an Id {editOrderRequest.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to update the order.");
        return response;
    }
}