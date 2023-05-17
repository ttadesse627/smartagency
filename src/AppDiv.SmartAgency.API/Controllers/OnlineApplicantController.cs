



using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Features.Command.Create.Applicants.OnlineApplicants;
using AppDiv.SmartAgency.Application.Features.Command.Create.Partners;
using AppDiv.SmartAgency.Application.Features.Command.Delete.Applicants;
using AppDiv.SmartAgency.Application.Features.Command.Delete.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Update.Partners;
using AppDiv.SmartAgency.Application.Features.Query.Applicants.OnlineApplicants;
using AppDiv.SmartAgency.Application.Features.Query.Customers;
using AppDiv.SmartAgency.Application.Features.Query.Partners;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

[ApiController]
[Route("api/onlineApplicant")]
    public class OnlineApplicantController: ControllerBase
{
    private readonly IMediator _mediator;
    public OnlineApplicantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<OnlineApplicantResponseDTO>> CreateOnlineApplicant(CreateOnlineApplicantCommand onlineApplicantRequest, CancellationToken token)
    {
        var response = await _mediator.Send(onlineApplicantRequest);
        return Ok(response);
    }



    [HttpGet("get-all-online-applicant")]
    public async Task<ActionResult<OnlineApplicantResponseDTO>> GetAllOnlineApplicants(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetAllOnlineApplicantQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

     [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OnlineApplicantResponseDTO> Get(Guid id)
        {
            return await _mediator.Send( new GetOnlineApplicantByIdQuery(id));
        }

      [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteOnlineApplicant(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteOnlineApplicantCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }



     
        
  
}
}

