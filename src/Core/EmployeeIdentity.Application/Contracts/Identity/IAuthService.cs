using EmployeeIdentity.Application.DTOs.Auth;

namespace EmployeeIdentity.Application.Contracts.Identity;

public interface IAuthService<T>
{
    Task<string> Register(RegisterDTO userDetails);
    Task<string> Login(LoginDTO loginDetails);
    Task<T> GetUserId(Guid userId);
}