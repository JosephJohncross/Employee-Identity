using System.ComponentModel;
using System.Net.Mime;
using Asp.Versioning;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Features.Auth.Commands.RequestPasswordReset;
using EmployeeIdentity.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeIdentity.API.EndpointClasses.Auth;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiVersion("1")]
[Route("api/v{v:apiVersion}/request_password_reset")]
public class RequestPasswordReset : ControllerBase
{
    private readonly ISender _sender;
    public RequestPasswordReset(ISender sender) => _sender = sender;
 
    [HttpPost(Name = "Request Password reset")]
    [Description("Request for a password reset link")]
    [SwaggerOperation(Tags = new[] { "Auth" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion(1)]
    // [Authorize]
    public async Task<ActionResult<BaseResponse>> HandleAsync([FromBody] RequestPasswordResetDTO passwordResetDTO)
    {
        var requestResetCommand = new RequestPasswordResetCommand(){passwordResetDTO = passwordResetDTO};
        return Ok(await _sender.Send(requestResetCommand));
    }
}