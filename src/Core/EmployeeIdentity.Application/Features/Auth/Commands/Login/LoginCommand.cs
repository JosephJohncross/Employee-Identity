using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Response;

namespace EmployeeIdentity.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<BaseResponse>
{
    public LoginDTO loginDetails { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse>
{
    private readonly IAuthService<string> _authService;
    public LoginCommandHandler(IAuthService<string> authService) => _authService = authService;

    public async Task<BaseResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.Login(request.loginDetails);
        return new BaseResponse {
            Data = new {
                accessToken = response,
            },
            Message = "Login successful",
            Status = true
        }; 
    }
}
