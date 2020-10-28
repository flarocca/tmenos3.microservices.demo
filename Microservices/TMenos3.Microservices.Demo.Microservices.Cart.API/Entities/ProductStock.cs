using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMenos3.Microservices.Demo.Microservices.Cart.API.Entities
{
    public class ProductStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        public int Quantity { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
