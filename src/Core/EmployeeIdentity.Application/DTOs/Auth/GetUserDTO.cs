using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeIdentity.Application.DTOs.Auth;

public class GetUserDTO
{
    public string Email { get; set; }
    public Guid Id { get; set; }
}