using System.ComponentModel;
using System.Net.Mime;
using Asp.Versioning;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Features.Auth.Commands.ResetPassword;
using EmployeeIdentity.Application.Response;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeIdentity.API.EndpointClasses.Auth;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiVersion("1")]
[Route("api/v{v:apiVersion}/reset_password")]
public class ResetPassword : ControllerBase
{
    private readonly ISender _mediator;
    public ResetPassword(ISender mediator) => _mediator = mediator;

    [HttpPost(Name = "Reset password")]
    [Description("Reset a user password")]
    [SwaggerOperation(Tags = new[] { "Auth" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion(1)]
    // [Authorize]
    public async Task<ActionResult<BaseResponse>> HandleAsync([FromQuery] ResetPasswordDTO resetPasswordDto){

        var resetPasswordCommand = new ResetPasswordCommand{resetPasswordDTO = resetPasswordDto};
        return Ok(await _mediator.Send(resetPasswordCommand));
    }
}