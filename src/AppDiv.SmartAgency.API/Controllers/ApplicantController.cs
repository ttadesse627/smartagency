

using AppDiv.SmartAgency.API.Middleware;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.EditApplicantRequests;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Create;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
using AppDiv.SmartAgency.Application.Features.Applicants.Queries;
using AppDiv.SmartAgency.Application.Features.Applicants.Query;
using AppDiv.SmartAgency.Utility.Contracts;

using Microsoft.AspNetCore.Mvc;


namespace AppDiv.SmartAgency.API.Controllers;

// [Authorize(JwtBearerDefaults.AuthenticationScheme)]

[ApiController]
[Route("api/applicant")]

//[AllowAnonymousAttribute]
public class ApplicantController : ApiControllerBase
{

    [HttpPost("create")]

    [RoleBasedAuthorizationMetadata("Applicant", "CanAdd")]
    public async Task<ActionResult<ServiceResponse<Int32>>> CreateApplicant(CreateApplicantRequest request)
    {
        var response = await Mediator.Send(new CreateApplicantCommand(request));
        return Ok(response);
    }

    [HttpGet("get-all")]

    [RoleBasedAuthorizationMetadata("Applicant", "CanView")]
    public async Task<ActionResult<ApplicantsResponseDTO>> GetAllApplicants(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await Mediator.Send(new GetAllApplicants(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }
    [RoleBasedAuthorizationMetadata("Applicant", "CanViewDetail")]
    [HttpGet("get/{id}")]
    public async Task<ActionResult<GetApplicantResponseDTO>> GetAllApplicants(Guid id)
    {
        return Ok(await Mediator.Send(new GetSingleApplicantQuery(id)));
    }


    [HttpDelete("delete/{id}")]

    [RoleBasedAuthorizationMetadata("Applicant", "CanDelete")]
    public async Task<ActionResult<ServiceResponse<Int32>>> DeleteApplicant(Guid id)
    {
        try
        {
            var response = await Mediator.Send(new DeleteApplicantCommand(id));
            return Ok(response);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("edit/{id}")]
    [RoleBasedAuthorizationMetadata("Applicant", "CanUpdate")]
    public async Task<ActionResult<ServiceResponse<Int32>>> EditApplicant(Guid id, [FromBody] EditApplicantRequest request)
    {
        try
        {
            if (request.Id == id)
            {
                var response = await Mediator.Send(new EditApplicantCommand(request));
                return Ok(response);
            }
            else return BadRequest();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("search-applicant")]
    public async Task<ActionResult<ApplSearchResponseDTO>> GetSearchResult
   (
       int pageNumber = 1, int pageSize = 20, string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending,
       Guid? jobTitleId = null, Guid? maritalStatusId = null, int ageFrom = 0, int ageTo = 0,
       Guid? religionId = null, Guid? experienceId = null, Guid? countryId = null
   )
    {
        return Ok(await Mediator.Send(new GetApplSearchResultQuery(
                        pageNumber, pageSize, orderBy, sortingDirection,
                        jobTitleId, maritalStatusId, ageFrom, ageTo,
                        religionId, experienceId, countryId
                    )));
    }

    [HttpPut("request/send-request")]
    public async Task<ActionResult<ServiceResponse<Int32>>> RequestApplicant(SendApplicantRequest request)
    {
        var response = await Mediator.Send(new RequestApplicantCommand(request));
        return Ok(response);
    }

    [HttpGet("request/get-all")]
    public async Task<ActionResult<ApplicantsResponseDTO>> GetAllRequestedApplicants(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await Mediator.Send(new GetAllRequestedQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

    [HttpDelete("request/delete/{id}")]
    public async Task<ActionResult<ServiceResponse<Int32>>> DeleteRequested(Guid id)
    {
        var response = await Mediator.Send(new DeleteRequestedCommand(id));
        return Ok(response);
    }

    [HttpGet("get-unassigned")]
    public async Task<ActionResult<GetUnAssignedApplicantsDTO>> GetUnassignedApplicants()
    {
        return Ok(await Mediator.Send(new GetUnassignedApplicantsQuery()));
    }


    //get applicant detail for cv

    [HttpGet("get-cv-detail/{id}")]

    public async Task<ActionResult<ApplicantCvResponseDTO>> GetApplicantCvDetail(Guid id)
    {
        return Ok(await Mediator.Send(new GetApplicantCvDetailQuery(id)));
    }

    [HttpGet("get-attachments")]

    public async Task<ActionResult<string>> GetApplicantAttachments(Guid ApplicantId, string AttachmentType)
    {
        return Ok(await Mediator.Send(new GetApplicantAttachmentsQuery(ApplicantId, AttachmentType)));
    }

    [HttpGet("get-unassigned-orders-dropdown")]
    public async Task<ActionResult<Object>> GetUnAssignedOrdersDropdown()
    {
        return Ok(await Mediator.Send(new GetUnAssignedOrdersDrodownQuery()));
    }

    [HttpGet("get-for-enjaz")]
    public async Task<ActionResult<ResponseContainerDTO<List<DropdownEnjazResponseDTO>>>> GetOrderForEnjaz()
    {
        var response = new ResponseContainerDTO<List<DropdownEnjazResponseDTO>>
        {
            Items = await Mediator.Send(new GetForEnjazQuery())
        };
        return Ok(response);
    }

    [HttpGet("get-travelled-applicants")]
    public async Task<ActionResult<TravelledApplicantsResponseDTO>> GetTravelledApplicants()
    {
        var result = await Mediator.Send(new GetTravelledApplicantsQuery());

        return Ok(result);
    }




    // [HttpGet("get-traveled-applicants")]
    // public async Task<ActionResult<SearchModel<DropdownEnjazResponseDTO>>> GetTraveledApplicants()
    // {
    //     var response = new ResponseContainerDTO<List<DropdownEnjazResponseDTO>>
    //     {
    //         Items = await Mediator.Send(new GetForEnjazQuery())
    //     };
    //     return Ok(response);
    // }

}