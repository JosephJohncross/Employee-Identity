using Microsoft.AspNetCore.Identity;

namespace EmployeeIdentity.Persistence.IdentityModels;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}