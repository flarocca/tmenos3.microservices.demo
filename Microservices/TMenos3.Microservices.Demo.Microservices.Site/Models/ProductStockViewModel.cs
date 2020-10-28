namespace TMenos3.Microservices.Demo.Microservices.Site.Models
{
    public class ProductStockViewModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
