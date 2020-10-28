using System.ComponentModel.DataAnnotations.Schema;

namespace TMenos3.Microservices.Demo.Microservices.Stock.Worker.Entities
{
    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
