using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Response;

namespace EmployeeIdentity.Application.Features.Auth.Commands.RequestPasswordReset;

public class RequestPasswordResetCommand : IRequest<BaseResponse>
{
    public RequestPasswordResetDTO passwordResetDTO {get; set;}
}

public class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand, BaseResponse>
{
    public readonly IAuthService<BaseResponse> _authService;
    public RequestPasswordResetCommandHandler(IAuthService<BaseResponse> authService) => _authService = authService;
   

    public async Task<BaseResponse> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        await _authService.RequestPasswordReset(request.passwordResetDTO);
        return new BaseResponse {
            Status =  true,
            Message = "Password rest link has been sent"            
        };
    }
}
