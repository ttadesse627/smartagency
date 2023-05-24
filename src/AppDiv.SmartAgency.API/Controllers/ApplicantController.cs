

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.EditApplicantRequests;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Create;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
using AppDiv.SmartAgency.Application.Features.Applicants.Queries;
using AppDiv.SmartAgency.Application.Features.Applicants.Query;
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
    public async Task<ActionResult<ServiceResponse<Int32>>> CreateApplicant(CreateApplicantRequest request)
    {
        var response = await _mediator.Send(new CreateApplicantCommand(request));
        return Ok(response);
    }
    [HttpGet("get-all")]
    public async Task<ActionResult<ApplicantsResponseDTO>> GetAllApplicants(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetAllApplicants(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<ApplicantsResponseDTO>> GetAllApplicants(Guid id)
    {
        return Ok(await _mediator.Send(new GetSingleApplicantQuery(id)));
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

    [HttpPut("edit/{id}")]
    public async Task<ActionResult<ServiceResponse<Int32>>> EditApplicant(Guid id, [FromBody] EditApplicantRequest request)
    {
        try
        {
            if (request.Id == id)
            {
                var response = await _mediator.Send(new EditApplicantCommand(request));
                return Ok(response);
            }
            else return BadRequest();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-for-assignment")]
    public async Task<ActionResult<List<GetForAssignmentDTO>>> GetOrderForAssignment()
    {
        return Ok(await _mediator.Send(new GetForAssignmentQuery()));
    }
}