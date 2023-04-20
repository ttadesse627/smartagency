
using AppDiv.CRVS.Application.Contracts.DTOs;
using AppDiv.CRVS.Application.Features.Customers.Command.Create;
using AppDiv.CRVS.Application.Features.Customers.Command.Delete;
using AppDiv.CRVS.Application.Features.Customers.Command.Update;
using AppDiv.CRVS.Application.Features.Customers.Query;
using AppDiv.CRVS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.CRVS.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin,Member")]
    // [Authorize]
    // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Member")]

    // Authorize with a specific scheme
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Member,User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _mediator;
        public CustomerController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<CustomerResponseDTO>> Get()
        {
            return await _mediator.Send(new GetAllCustomerQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Customer> Get(Int64 id)
        {
            return await _mediator.Send(new GetCustomerByIdQuery(id));
        }

        [HttpGet("email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Customer> GetByEmail(string email)
        {
            return await _mediator.Send(new GetCustomerByEmailQuery(email));
        }

        [HttpPost("Create")]
      // [ProducesResponseType(StatusCodes.Status200OK)]
      // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CustomerResponseDTO>> CreateCustomer([FromBody] CreateCustomerCommand command,CancellationToken token)
        {
            var result = await _mediator.Send(command,token);
            return Ok(result);
        }


        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditCustomerCommand command)
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
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }


        [Authorize(Roles = "Admin, Management")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteCustomerCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

    }
}