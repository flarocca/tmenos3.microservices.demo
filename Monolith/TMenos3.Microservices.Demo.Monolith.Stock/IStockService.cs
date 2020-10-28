using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;

namespace TMenos3.Microservices.Demo.Monolith.Stock
{
    public interface IStockService
    {
        Task<ProductStock> AddAsync(int productId, int quantity);
        Task<ProductStock> GetAsync(int id);
        Task<ProductStock> GetByProductAsync(int productId);
    }
}