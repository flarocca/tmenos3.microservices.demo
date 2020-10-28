using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;

namespace TMenos3.Microservices.Demo.Monolith.Prices
{
    public interface IPriceService
    {
        Task<ProductPrice> AddAsync(int productId, int value);
        Task<ProductPrice> GetAsync(int id);
        Task<ProductPrice> GetByProductAsync(int productId);
    }
}