namespace TMenos3.Microservices.Demo.Microservices.Messaging.Events
{
    public class PriceForProductCreated : IEvent
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Price { get; set; }
    }
}
