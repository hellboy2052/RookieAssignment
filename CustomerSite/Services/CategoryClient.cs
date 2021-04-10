using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ShareVM;

namespace CustomerSite.Services
{
    public class CategoryClient : ICategoryClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public CategoryClient(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<CategoryVm>> GetCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_config["API:Default"] + "/Categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}