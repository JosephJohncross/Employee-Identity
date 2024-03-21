
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Persistence.IdentityModels;
using EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using EmployeeIdentity.Persistence.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Policy;
using EmployeeIdentity.Application.Contracts.Infrastructure;
using EmployeeIdentity.Application.Models.Mail;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using EmployeeIdentity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EmployeeIdentity.Infrastructure.Contracts;

public class AuthRepository<T> : IAuthService<T>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly IMail _mailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;

    public AuthRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IConfiguration config,
            IMail mailSender,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context,
            ILogger logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _config = config;
        _mailSender = mailSender;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _logger = logger;
    }

    public async Task<T> GetUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new IdentityNotFoundException($"User with id {userId} not found");
        }

        return _mapper.Map<T>(user);
    }

    public async Task<string> Login(LoginDTO loginDetails)
    {
        var user = await _userManager.FindByEmailAsync(loginDetails.Email) ?? throw new IdentityNotFoundException("Invalid login credentials");
        var result = await _signInManager.PasswordSignInAsync(user, loginDetails.Password, loginDetails.RememberMe, false);
        if (result.Succeeded)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var token = AuthenticationService.CreateToken(claims, _config);

            return token;
        }

        throw new IdentityForbiddenException("Invalid login credentials");
    }

    public async Task<bool> PasswordReset(ResetPasswordDTO passwordResetDTO)
    {
        var user = await  _userManager.FindByIdAsync(passwordResetDTO.UserId.ToString()) ?? throw new IdentityForbiddenException("You are not unauthorized");
        var isValidToken = await _userManager.VerifyUserTokenAsync(
            user, 
            _userManager.Options.Tokens.PasswordResetTokenProvider,
            "ResetPassword",
            passwordResetDTO.Token
            );

        if (!isValidToken) {
            throw new IdentityBadRequestException("Token is invalid or has expired");
        }

        var resetResult = await _userManager.ResetPasswordAsync(user, passwordResetDTO.Token, passwordResetDTO.NewPassword);
        if (!resetResult.Succeeded){
            throw new IdentityBadRequestException("Password reset failed");
        }
        return true;
    }

    public async Task<string> Register(RegisterDTO userDetails)
    {
        var existingUser = await _userManager.FindByEmailAsync(userDetails.Email);
        if (existingUser != null)
        {
            throw new IdentityUserExistException("User already exist");
        }

        var user = new ApplicationUser
        {
            Email = userDetails.Email,
            FirstName = userDetails.FirstName,
            LastName = userDetails.LastName
        };

        // await _context.SaveChangesAsync();

        // await _userManager.CreateAsync(user, userDetails.Password);
        var passwordHash = _userManager.PasswordHasher.HashPassword(user, userDetails.Password);
        user.PasswordHash = passwordHash;
        user.NormalizedEmail = userDetails.Email.ToUpper();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task RequestPasswordReset(RequestPasswordResetDTO passwordResetDTO)
    {
        var user = await _userManager.FindByEmailAsync(passwordResetDTO.Email) ?? throw new IdentityNotFoundException("User does not exist");
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/v1/reset_password?token={UrlEncoder.Default.Encode(token)}";

        SendEmailParameters sendEmailParameters = new SendEmailParameters
        {
            Body = $"{resetLink}",
            From = _config.GetSection("EmailSettings").GetValue<string>("FromAddress") ?? "",
            Message = "Please click on the link to reset your password",
            Subject = "Password Reset",
            To = user.Email
        };

        await _mailSender.SendEmailAsync(sendEmailParameters);
    }
}