using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Update
{
    public class EditLookUpCommand : IRequest<LookUpResponseDTO>
    {


        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Value { get; set; }
    }


    public class EditLookUpCommandHandler : IRequestHandler<EditLookUpCommand, LookUpResponseDTO>
    {
        private readonly ILookUpRepository _lookUpRepository;
        private readonly ILookUpRepository _lookUpQueryRepository;
        public EditLookUpCommandHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<LookUpResponseDTO> Handle(EditLookUpCommand request, CancellationToken cancellationToken)
        {
            // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request);
            LookUp lookUpEntity = new LookUp
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                Value = request.Value

            };

            try
            {
                await _lookUpRepository.UpdateAsync(lookUpEntity, x => x.Id);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            var modifiedLookUp = await _lookUpQueryRepository.GetByIdAsync(request.Id);
            var lookUpResponse = CustomMapper.Mapper.Map<LookUpResponseDTO>(modifiedLookUp);

            return lookUpResponse;
        }
    }

}