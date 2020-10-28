using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Cart;
using TMenos3.Microservices.Demo.Monolith.Site.Models;

namespace TMenos3.Microservices.Demo.Monolith.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICartService carService;

        public ProductsController(ICartService carService) =>
            this.carService = carService;

        public async Task<ActionResult> Index()
        {
            var products = await carService.GetAllAsync();
            var model = new ProductListViewModel
            {
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Price = p.Price.Value,
                    Stock = p.Stock.Quantity
                })
            };
            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var product = await carService.GetAsync(id);
            var model = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price.Value,
                Stock = product.Stock.Quantity
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel model)
        {
            try
            {
                await carService.CreateAsync(model.Description, model.Price, model.Stock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPut("products")]
        public IActionResult SetError()
        {
            carService.FeatureToggle();
            return NoContent();
        }
    }
}
