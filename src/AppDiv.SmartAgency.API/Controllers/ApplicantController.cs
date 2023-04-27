

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Features.Command.Create.Applicants;
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
    public async Task<ActionResult<ServiceResponse<CreateApplicantResponseDTO>>> CreateApplicant(CreateApplicantCommand request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}