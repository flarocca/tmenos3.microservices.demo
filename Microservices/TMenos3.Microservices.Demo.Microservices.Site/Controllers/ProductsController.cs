using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Site.Models;
using TMenos3.Microservices.Demo.Microservices.Site.Services;

namespace TMenos3.Microservices.Demo.Microservices.Site.Controllers
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
                    Price = p.Price,
                    Stock = p.Stock
                })
            };

            Response.Headers.Add("Refresh", "2");
            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var product = await carService.GetAsync(id);
            var model = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
            return View(model);
        }

        public ActionResult Create() =>
            View(new ProductViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel model)
        {
            await carService.CreateAsync(model.Description, model.Price, model.Stock);
            return RedirectToAction(nameof(Index));
        }
    }
}
