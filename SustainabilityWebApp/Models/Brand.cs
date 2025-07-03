using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SustainabilityWebApp.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly FoundedDate { get; set; }

        public float Score { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AveragePrice { get; set; }

    }
}
