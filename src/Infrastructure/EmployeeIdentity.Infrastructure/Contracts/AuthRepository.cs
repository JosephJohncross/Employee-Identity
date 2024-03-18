
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Persistence.IdentityModels;
using EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using EmployeeIdentity.Persistence.Services;
using Microsoft.Extensions.Configuration;

namespace EmployeeIdentity.Infrastructure.Contracts;

public class AuthRepository<T> : IAuthService<T>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _config = config;
    }

    public async Task<T> GetUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null){
            throw new IdentityNotFoundException($"User with id {userId} not found");
        }
        
        return _mapper.Map<T>(user);
    }

    public async Task<string> Login(LoginDTO loginDetails)
    {
        var user = await _userManager.FindByEmailAsync(loginDetails.Email) ?? throw new IdentityNotFoundException("Invalid login credentials");

        var result = await _signInManager.PasswordSignInAsync(user, loginDetails.Password, loginDetails.RememberMe, false);

        if (result.Succeeded){
            var claims = await _userManager.GetClaimsAsync(user);
            var token = AuthenticationService.CreateToken(claims, _config);

            return token;
        }
        
        throw new IdentityForbiddenException("Invalid login credentials");
    }

    public async Task<string> Register(RegisterDTO userDetails)
    {
        var existingUser = await _userManager.FindByEmailAsync(userDetails.Email);
        if (existingUser != null) {
            throw new IdentityUserExistException("User already exist");
        }

        var user = new ApplicationUser{
            Email = userDetails.Email,
        };

        await _userManager.CreateAsync(user, userDetails.Password);
        return user.Id;
    }
}