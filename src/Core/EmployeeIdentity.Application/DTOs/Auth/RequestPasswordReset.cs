using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeIdentity.Application.DTOs.Auth
{
    public class RequestPasswordResetDTO
    {
         public required string Email { get; set; }
    }
}