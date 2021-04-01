using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public class CategoryClient : ICategoryClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<CategoryVm>> GetCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5002/Categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}