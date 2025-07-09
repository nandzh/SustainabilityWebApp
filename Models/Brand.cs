using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SustainabilityWebApp.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CountryOfOrigin { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^(10(\.0+)?|[1-5](\.\d+)?)$", ErrorMessage = "Enter a number between 1 and 5")]
        public double PlanetSustainabilityRate { get; set; }
        
        [Required]
        [RegularExpression(@"^(10(\.0+)?|[1-5](\.\d+)?)$", ErrorMessage = "Enter a number between 1 and 5")]
        public double PeopleRate { get; set; }
        
        [Required]
        [RegularExpression(@"^(10(\.0+)?|[1-5](\.\d+)?)$", ErrorMessage = "Enter a number between 1 and 5")]
        public double AnimalCrueltyRate { get; set; }
        
        [Required]
        //[RegularExpression(@"^\${1,4}$", ErrorMessage = "Select between one and four dollar signs.")]
        public int? PricingRate { get; set; }
        
        [Required]
        public string SmileyFace { get; set; } = string.Empty;
        
        [Required]
        //[RegularExpression(@"^(We avoid|Not good enough|It's a start|Good|Great)$")]
        public string Rated { get; set; } = string.Empty;
        
        public string? LogoUrl { get; set; } = string.Empty;
        

    }
}
