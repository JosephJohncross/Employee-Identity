using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Application.Response;

namespace EmployeeIdentity.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<BaseResponse>
{
    public ResetPasswordDTO resetPasswordDTO { get; set; }
}

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseResponse>
{
    private readonly IAuthService<bool> _authService;
    public ResetPasswordCommandHandler(IAuthService<bool> authService) => _authService =authService;

    public async Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        await _authService.PasswordReset(request.resetPasswordDTO);
        
        return new BaseResponse {
            Status = true,
            Message = "Password has been reset successfully"
        };
    }
}
