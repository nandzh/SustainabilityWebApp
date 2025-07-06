using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SustainabilityWebApp.Models
{
    public class Brand
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; }
        
        public string CountryOfOrigin { get; set; }
        
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
        [RegularExpression(@"^\${1,4}$", ErrorMessage = "Select between one and four dollar signs.")]
        public int PricingRate { get; set; } // 1 to 4 for $ to $$$$
        
        [Required]
        public string SmileyFace { get; set; }
        
        [Required]
        [RegularExpression(@"^(We avoid|Not good enough|It's a start|Good|Great)$")]
        public string Rated { get; set; }
        

    }
}
