using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SustainabilityWebApp.Models;

namespace SustainabilityWebApp.Data
{
    public class SustainabilityWebAppContext : DbContext
    {
        public SustainabilityWebAppContext (DbContextOptions<SustainabilityWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<SustainabilityWebApp.Models.Brand> Brand { get; set; } = default!;
    }
}
