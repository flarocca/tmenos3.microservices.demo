using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Site.Models;

namespace TMenos3.Microservices.Demo.Microservices.Site.Services
{
    public interface ICartService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel> GetAsync(int id);

        Task CreateAsync(string description, int price, int stock);
    }
}