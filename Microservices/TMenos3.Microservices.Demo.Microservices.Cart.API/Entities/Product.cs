using System.ComponentModel.DataAnnotations.Schema;

namespace TMenos3.Microservices.Demo.Microservices.Cart.API.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public ProductStock Stock { get; set; }

        public ProductPrice Price { get; set; }
    }
}
