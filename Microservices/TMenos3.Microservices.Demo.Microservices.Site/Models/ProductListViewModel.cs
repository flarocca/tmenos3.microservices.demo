using System.Collections.Generic;

namespace TMenos3.Microservices.Demo.Microservices.Site.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
