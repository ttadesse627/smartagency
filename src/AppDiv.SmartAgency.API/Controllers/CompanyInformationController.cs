
using AppDiv.SmartAgency.API.Middleware;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Create;
using AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Update;
using AppDiv.SmartAgency.Application.Features.CompanyInformations.Query;
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

        [AllowAnonymousAttribute]

        [HttpPost("create")]
        public async Task<ActionResult> CreateCompanyInformation([FromBody] CreateCompanyInformationRequest companyInformationRequest)
        {
            var response = await _mediator.Send(new CreateCompanyInformationCommand(companyInformationRequest));
            return Ok(response);
        }



        [HttpGet("get-company-information")]
        public async Task<ActionResult<GetCompanyInformationResponseDTO>> GetCompanyInformation()
        {
            return Ok(await _mediator.Send(new GetCompanyInformationQuery()));
        }
        /*
                [HttpGet("{id}")]
                [ProducesResponseType(StatusCodes.Status200OK)]
                public async Task<OnlineApplicantResponseDTO> Get(Guid id)
                {
                    return await _mediator.Send(new GetOnlineApplicantByIdQuery(id));
                }

               */


        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditCompanyInformationCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

                //var result = await _mediator.Send(command,id);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }


        }
    }

}

