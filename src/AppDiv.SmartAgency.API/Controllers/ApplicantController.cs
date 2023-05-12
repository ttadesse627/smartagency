

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Features.Command.Create.Applicants;
using AppDiv.SmartAgency.Application.Features.Command.Update.Applicants;
using AppDiv.SmartAgency.Application.Features.Query.Applicants;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;
[ApiController]
[Route("api/applicant")]
public class ApplicantController : ControllerBase
{
    private readonly IMediator _mediator;
    public ApplicantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<CreateApplicantResponseDTO>>> CreateApplicant(CreateApplicantRequest request)
    {
        var response = await _mediator.Send(new CreateApplicantCommand(request));
        return Ok(response);
    }
    [HttpGet("get-all")]
    public async Task<ActionResult<ApplicantsResponseDTO>> GetAllApplicants()
    {
        return Ok(await _mediator.Send(new GetAllApplicants()));
    }
    [HttpPut("delete/{id}")]
    public async Task<ActionResult<ServiceResponse<Int32>>> DeleteApplicant(Guid id, [FromBody] DeleteApplicantCommand command)
    {
        try
        {
            if (command.Id == id)
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            else return BadRequest();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search&sortapplicant")]
    public async Task<ActionResult<ApplicantsResponseDTO>> SearchApplicants
        (
            [FromQuery] string searchTerm,
            [FromQuery] string orderBy = "FirstName",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] SortingDirection sortOrderAscending = SortingDirection.Ascending

        )
    {
        return Ok(await _mediator.Send(new SearchApplicants(searchTerm, orderBy, pageNumber, pageSize, sortOrderAscending)));
    }
}