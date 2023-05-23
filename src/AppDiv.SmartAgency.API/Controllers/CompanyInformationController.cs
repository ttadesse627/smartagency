
using AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs;
using AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

    [ApiController]
    [Route("api/companyInformation")]
    public class CompanyInformationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyInformationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CompanyInformationResponseDTO>> CreateCompanyInformation(CreateCompanyInformationCommand companyInformationRequest, CancellationToken token)
        {
            var response = await _mediator.Send(companyInformationRequest);
            return Ok(response);
        }
/*
        [HttpGet("get-all-online-applicant")]
        public async Task<ActionResult<OnlineApplicantResponseDTO>> GetAllOnlineApplicants(int pageNumber = 1, int pageSize = 15, string? searchTerm = null, string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllOnlineApplicantQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OnlineApplicantResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetOnlineApplicantByIdQuery(id));
        }

       */
    }
}

