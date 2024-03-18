
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Response;

namespace EmployeeIdentity.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<BaseResponse>
{
    public RegisterDTO userDetails { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, BaseResponse>
{

    private readonly IAuthService<BaseResponse> _authService;
    public RegisterCommandHandler(IAuthService<BaseResponse> authService) => _authService = authService;

    public async Task<BaseResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.Register(request.userDetails);
        
        return new BaseResponse {
            Status = true,
            Data = response,
            Message = "User created successfully"
        };
    }
}
