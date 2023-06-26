using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;

namespace AppDiv.SmartAgency.Application.Features.Dashbourds.Query
{
    public class GetDashbourdQuery: IRequest<List<DashbourdResponseDTO>>
    {
        
    }

    public class GetDashbourdHandler: IRequestHandler<GetDashbourdQuery , List<DashbourdResponseDTO>>
    {

       private readonly  IProcessDefinitionRepository _processDefinitionRepository;
       private readonly  IApplicantProcessRepository _applicantProcessRepository;
       
       public GetDashbourdHandler(IProcessDefinitionRepository processDefinitionRepository, IApplicantProcessRepository applicantProcessRepository)
       {
          _processDefinitionRepository= processDefinitionRepository;  
          _applicantProcessRepository= applicantProcessRepository;
       }

       public async Task<List<DashbourdResponseDTO>> Handle(GetDashbourdQuery request, CancellationToken cancellationToken)
       {

           var applicantProcesses = await _applicantProcessRepository.GetAllAsync();

            var groupedApplicantProcesses = applicantProcesses
        .GroupBy(ap => ap.ProcessDefinitionId)
        .Select(g => new { ProcessDefinitionId = g.Key, Count = g.Count() });      


        return CustomMapper.Mapper.Map<List<DashbourdResponseDTO>>(groupedApplicantProcesses);

        // return groupedApplicantProcesses.ToList();
        
    




   
       }   
    }
}
