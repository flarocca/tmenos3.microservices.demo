using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMenos3.Microservices.Demo.Monolith.Database.Entities
{
    public class ProductPrice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Value { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
