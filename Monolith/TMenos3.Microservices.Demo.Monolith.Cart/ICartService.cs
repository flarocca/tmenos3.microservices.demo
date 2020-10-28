using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;

namespace TMenos3.Microservices.Demo.Monolith.Cart
{
    public interface ICartService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetAsync(int id);

        Task CreateAsync(string description, int price, int stock);

        void FeatureToggle();
    }
}