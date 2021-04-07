using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public class BrandClient : IBrandClient
    {
        private readonly IHttpClientFactory _clientFactory;
        public BrandClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IList<BrandVm>> GetBrands()
        {
            var clients = _clientFactory.CreateClient();
            var response = await clients.GetAsync("https://localhost:5002/Brand");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<BrandVm>>();
        }
    }
}