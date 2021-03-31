using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public class BrandClient : IBrandClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<BrandVm>> GetBrands()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5002/Brands");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<BrandVm>>();
        }
    }
}