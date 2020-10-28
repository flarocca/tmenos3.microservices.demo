using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Site.Models;

namespace TMenos3.Microservices.Demo.Microservices.Site.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient httpClient;

        public CartService(HttpClient httpClient) =>
            this.httpClient = httpClient;

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            try
            {
                using var response = await httpClient.GetAsync("products");
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(json);
            }
            catch (Exception)
            {
                return Array.Empty<ProductViewModel>();
            }
        }

        public async Task<ProductViewModel> GetAsync(int id)
        {
            try
            {
                using var response = await httpClient.GetAsync($"products/{id}");
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ProductViewModel>(json);
            }
            catch (Exception)
            {
                return new ProductViewModel();
            }
        }

        public async Task CreateAsync(string description, int price, int stock)
        {
            var request = new
            {
                description,
                price,
                stock
            };

            var json = JsonConvert.SerializeObject(request);

            using var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            using var response = await httpClient.PostAsync($"products", content);
        }
    }
}
