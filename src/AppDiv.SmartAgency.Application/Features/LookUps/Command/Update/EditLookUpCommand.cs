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

        var lookUpResponse= new LookUpResponseDTO();

            try
            {
              var res=  await _lookUpRepository.UpdateAsync(lookUpEntity);

                if(res>=1){
                    var modifiedLookUp =await _lookUpRepository.GetWithPredicateAsync(l=>l.Id==request.Id, "Category");
                    lookUpResponse= CustomMapper.Mapper.Map<LookUpResponseDTO>(modifiedLookUp);
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

           
            return lookUpResponse;
        }
    }

}