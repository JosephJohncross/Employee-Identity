
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;

namespace EmployeeIdentity.Application.Features.Auth.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<GetUserDTO>
{
    public Guid UserId { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserDTO>
{
    private readonly IAuthService<GetUserDTO> _authService;
    public GetUserByIdQueryHandler(IAuthService<GetUserDTO> authService)
    {
            _authService = authService;
        
    }
    public async Task<GetUserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _authService.GetUserId(request.UserId);
        return response;
    }
}
