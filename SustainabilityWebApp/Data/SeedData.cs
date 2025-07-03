using Microsoft.EntityFrameworkCore;
using SustainabilityWebApp.Models;

namespace SustainabilityWebApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SustainabilityWebAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SustainabilityWebAppContext>>());

            if (context == null || context.Brand == null)
            {
                throw new NullReferenceException("Null SustainabilityWebAppContext or Brand DbSet");

            }

            if (context.Brand.Any())
            {
                return;
            }

            context.Brand.AddRange(
                new Brand
                {
                    Name = "Nike",
                    FoundedDate = new DateOnly(1111, 11, 11),
                    Score = 1,
                    AveragePrice = 1
                },
                new Brand
                {
                    Name = "Lacoste",
                    FoundedDate = new DateOnly(1111, 11, 11),
                    Score = 1,
                    AveragePrice = 1
                },
                new Brand
                {
                    Name = "Puma",
                    FoundedDate = new DateOnly(1111, 11, 11),
                    Score = 1,
                    AveragePrice = 1
                },
                new Brand
                {
                    Name = "Gucci",
                    FoundedDate = new DateOnly(1111, 11, 11),
                    Score = 1,
                    AveragePrice = 1
                },
                new Brand
                {
                    Name = "Dolce & Gabanna",
                    FoundedDate = new DateOnly(1111, 11, 11),
                    Score = 1,
                    AveragePrice = 1
                },
                new Brand
                {
                    Name = "Louis Vitton",
                    FoundedDate = new DateOnly(1111, 11, 11),
                    Score = 1,
                    AveragePrice = 1
                }
                );
            context.SaveChanges();
        }
    }
}
