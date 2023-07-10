using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using Newtonsoft.Json.Linq;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public record GetApplicantCvDetailQuery(Guid Id):IRequest<ApplicantCvResponseDTO>
    {

    }

    public class GetApplicantCvDetailHandler : IRequestHandler<GetApplicantCvDetailQuery, ApplicantCvResponseDTO>
    {
        private readonly IApplicantRepository _applicantRepository;
        
        public GetApplicantCvDetailHandler(IApplicantRepository applicantRepository)
        {
            _applicantRepository= applicantRepository;
        }
      public async Task<ApplicantCvResponseDTO> Handle(GetApplicantCvDetailQuery request, CancellationToken cancellationToken)
      {
        var response = await _applicantRepository.GetApplicantCvDetail(request.Id);
           return response;
           


      }  

    } 
}