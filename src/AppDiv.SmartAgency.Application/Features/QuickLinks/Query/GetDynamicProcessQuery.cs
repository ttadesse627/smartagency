using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.QuickLinks.Query
{
    public class GetDynamicProcessQuery :IRequest<List<DynamicProcessResponseDTO>>
    {
        public Guid Id{get; set;}
        public GetDynamicProcessQuery(Guid id)
        {
            Id = id;
        }

    }
   
    public class GetDynamicProcessHandler :IRequestHandler<GetDynamicProcessQuery, List<DynamicProcessResponseDTO>>
    {
        private readonly IProcessDefinitionRepository _processDefnitionRepository;
        public GetDynamicProcessHandler(IProcessDefinitionRepository processDefinitionRepository)
        {
            _processDefnitionRepository= processDefinitionRepository;
        }
        
        public async Task<List<DynamicProcessResponseDTO>>  Handle(GetDynamicProcessQuery request, CancellationToken cancellationToken)
        {
            return await _processDefnitionRepository.GetDynamicProcesses(request.Id);
        }


    }



}