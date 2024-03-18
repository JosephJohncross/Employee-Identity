using System.ComponentModel;
using System.Net.Mime;
using Asp.Versioning;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Features.Auth.Commands.Register;
using EmployeeIdentity.Application.Response;

namespace EmployeeIdentity.API.EndpointClasses.Auth;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiVersion("1")]
[Route("api/v{v:apiVersion}/register")]
public class Register : ControllerBase
{
    private readonly ISender _mediator;
    public Register(ISender mediator) => _mediator = mediator;

    [HttpPost(Name = "Register")]
    [Description("Deletes a department in the organization")]
    [SwaggerOperation(Tags = new[] { "Auth"})]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion(1)]
    public async Task<ActionResult<BaseResponse>> HandleAsync(RegisterDTO registerDetails){
        var registerCommand = new RegisterCommand{
            userDetails =  registerDetails
        };
        var response = await _mediator.Send(registerCommand);

        return Ok(response);
    }
}