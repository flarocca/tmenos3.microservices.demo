using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMenos3.Microservices.Demo.Microservices.Cart.Worker.Entities
{
    public class ProductPrice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        public int Value { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
