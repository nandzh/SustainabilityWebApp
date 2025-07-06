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
        public DateOnly FoundedDate { get; set; }

        [Required]
        [RegularExpression(@"^(10(\.0+)?|[1-9](\.\d+)?)$", ErrorMessage = "Enter a number between 1 and 10")]
        public float Score { get; set; }

        [Required]
        [RegularExpression(@"^\${1,4}$", ErrorMessage = "Select between one and four dollar signs.")]
        public string AveragePrice { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(Terrible|Bad|Getting There|Good|Amazing)$")]
        public string? Rating { get; set; }

    }
}
