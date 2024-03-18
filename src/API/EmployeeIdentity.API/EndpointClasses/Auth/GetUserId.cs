using System.ComponentModel;
using System.Net.Mime;
using Asp.Versioning;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Features.Auth.Queries.GetUserById;
using EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware;

namespace EmployeeIdentity.API.EndpointClasses.Auth;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiVersion("1")]
[Route("api/v{v:apiVersion}/get_user_id")]
public class GetUserId : ControllerBase
{
    private readonly ISender _mediator;
    public GetUserId(ISender mediator) => _mediator = mediator;

    [HttpGet(Name = "GetUserId")]
    [Description("Gets a user by id")]
    [SwaggerOperation(Tags = ["Auth"])]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion(1)]
    public async Task<ActionResult<GetUserDTO>> HandleAsync(Guid userId)
    {
        if (userId.Equals(Guid.Empty)){
            throw new IdentityBadRequestException("User id is required");
        } 

        var getUserByIdQuery = new GetUserByIdQuery { UserId = userId };
        return Ok(await _mediator.Send(getUserByIdQuery));
    }

}