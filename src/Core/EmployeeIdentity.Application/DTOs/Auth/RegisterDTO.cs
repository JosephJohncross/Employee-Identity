namespace EmployeeIdentity.Application.DTOs.Auth;

public class RegisterDTO
{
    public required string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public required string Password { get; set; }
}