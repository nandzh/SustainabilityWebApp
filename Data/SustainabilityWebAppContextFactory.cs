using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SustainabilityWebApp.Data;

namespace SustainabilityWebApp.Data
{
    public class SustainabilityWebAppContextFactory : IDesignTimeDbContextFactory<SustainabilityWebAppContext>
    {
        public SustainabilityWebAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SustainabilityWebAppContext>();
            optionsBuilder.UseSqlServer("Server=tcp:sustainabilityweb.database.windows.net,1433;Initial Catalog=sustainabilitydatabase;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;");

            return new SustainabilityWebAppContext(optionsBuilder.Options);
        }
    }
}