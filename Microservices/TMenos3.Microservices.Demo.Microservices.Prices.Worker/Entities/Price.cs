using System.ComponentModel.DataAnnotations.Schema;

namespace TMenos3.Microservices.Demo.Microservices.Prices.Worker.Entities
{
    public class Price
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Value { get; set; }

        public int ProductId { get; set; }
    }
}
