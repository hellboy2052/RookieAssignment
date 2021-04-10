using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ShareVM;

namespace CustomerSite.Services
{
    public class BrandClient : IBrandClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public BrandClient(IHttpClientFactory clientFactory, IConfiguration config)
        {
            this._config = config;
            _clientFactory = clientFactory;
        }

        public async Task<IList<BrandVm>> GetBrands()
        {
            var clients = _clientFactory.CreateClient();
            var response = await clients.GetAsync(_config["API:Default"] + "/Brand");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<BrandVm>>();
        }
    }
}