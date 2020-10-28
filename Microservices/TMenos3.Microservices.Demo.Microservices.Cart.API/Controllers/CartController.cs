using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Cart.API.Models;
using TMenos3.Microservices.Demo.Microservices.Cart.API.Services;

namespace TMenos3.Microservices.Demo.Microservices.Cart.API.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;

        public CartController(ICartService service) =>
            this.service = service;

        [HttpGet("products")]
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var result = await service.GetAllAsync();

            return result.Select(r => new ProductViewModel
            {
                Id = r.Id,
                Description = r.Description,
                Price = r.Price != null ? r.Price.Value : -1,
                Stock = r.Stock != null ? r.Stock.Quantity : -1
            });
        }

        [HttpGet("products/{id:int}", Name = nameof(GetAsync))]
        public async Task<ProductViewModel> GetAsync(int id)
        {
            var result = await service.GetAsync(id);

            return new ProductViewModel
            {
                Id = result.Id,
                Description = result.Description,
                Price = result.Price != null ? result.Price.Value : -1,
                Stock = result.Stock != null ? result.Stock.Quantity : -1
            };
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateAsync(CreateProductViewModel model)
        {
            var id = await service.CreateAsync(model.Description, model.Price, model.Stock);

            return CreatedAtRoute(nameof(GetAsync), new { id, version = "1" }, id);
        }

        [HttpPut("products")]
        public IActionResult SetError()
        {
            service.FeatureToggle();
            return NoContent();
        }
    }
}
