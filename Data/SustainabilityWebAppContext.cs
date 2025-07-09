using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SustainabilityWebApp.Models;
using SustainabilityWebApp.Components.Account; // This is where ApplicationUser is defined

namespace SustainabilityWebApp.Data
{
    public class SustainabilityWebAppContext : IdentityDbContext<ApplicationUser>
    {
        public SustainabilityWebAppContext(DbContextOptions<SustainabilityWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brand { get; set; } = default!;
    }
}