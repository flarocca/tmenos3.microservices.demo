namespace TMenos3.Microservices.Demo.Microservices.Messaging.Events
{
    public class StockForProductCreated : IEvent
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
