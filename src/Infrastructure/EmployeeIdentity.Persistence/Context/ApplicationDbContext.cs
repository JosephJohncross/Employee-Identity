using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeIdentity.Persistence.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeIdentity.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // public DbSet<ApplicationUser> User { get; set; }
    }
}