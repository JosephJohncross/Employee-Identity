using System.ComponentModel;
using System.Net.Mime;
using Asp.Versioning;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Features.Auth.Commands.Login;
using EmployeeIdentity.Application.Response;

namespace EmployeeIdentity.API.EndpointClasses.Auth;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiVersion("1")]
[Route("api/v{v:apiVersion}/login")]
public class Login : ControllerBase
{
    private readonly ISender _mediator;
    public Login(ISender mediator) => _mediator = mediator; 

    [HttpPost(Name = "Login")]
    [Description("Endpoint to login a user")]
    [SwaggerOperation(Tags = new[] { "Auth"})]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [MapToApiVersion(1)]
    public async Task<ActionResult<BaseResponse>> HandleAsync([FromBody] LoginDTO loginDetails)
    {
        var loginCommand = new LoginCommand{loginDetails = loginDetails};

        return Ok(await _mediator.Send(loginCommand));
    }
}