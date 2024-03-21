namespace EmployeeIdentity.Application.DTOs.Auth;

public class ResetPasswordDTO
{
    public required Guid UserId { get; set; }
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
}