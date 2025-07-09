using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Globalization;
using System.IO;
using SustainabilityWebApp.Models;

namespace SustainabilityWebApp.Data
{
    public static class SeedData
    {
        public static void SeedFromExcel(IServiceProvider serviceProvider, string filePath)
        {
            using var context = new SustainabilityWebAppContext(
                serviceProvider.GetRequiredService<DbContextOptions<SustainabilityWebAppContext>>());

            if (context == null || context.Brand == null)
            {
                throw new NullReferenceException("Null SustainabilityWebAppContext or Brand DbSet");
            }
            
            if (context.Brand.Any())
            {
                return;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var fileInfo = new FileInfo(filePath);
            using var package = new ExcelPackage(fileInfo);
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) 
            {
                var pricingString = worksheet.Cells[row, 6].Text.Trim();
                int? pricingRate = string.IsNullOrEmpty(pricingString) ? null : pricingString.Count(c => c == '$');

                var brand = new Brand
                {
                    Name = worksheet.Cells[row, 1].Text.Trim(),
                    CountryOfOrigin = worksheet.Cells[row, 2].Text.Trim(),
                    PlanetSustainabilityRate = double.TryParse(worksheet.Cells[row, 3].Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var psr) ? psr : 0,
                    PeopleRate = double.TryParse(worksheet.Cells[row, 4].Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var pr) ? pr : 0,
                    AnimalCrueltyRate = double.TryParse(worksheet.Cells[row, 5].Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var acr) ? acr : 0,
                    PricingRate = pricingRate,
                    SmileyFace = worksheet.Cells[row, 7].Text.Trim(),
                    Rated = worksheet.Cells[row, 8].Text.Trim(),
                    LogoUrl = worksheet.Cells[row, 9].Text.Trim()
                };

                context.Brand.Add(brand);
            }

            context.SaveChanges();
        }
    }
}
