using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Cart.API.Entities;

namespace TMenos3.Microservices.Demo.Microservices.Cart.API.Services
{
    public interface ICartService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetAsync(int id);

        Task<int> CreateAsync(string description, int price, int stock);

        void FeatureToggle();
    }
}