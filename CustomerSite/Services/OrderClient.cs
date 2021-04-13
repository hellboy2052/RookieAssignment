using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerSite.Services.Interface;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShareVM;

namespace CustomerSite.Services
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public OrderClient(IHttpClientFactory httpClientFactory, IConfiguration config,
        IHttpContextAccessor httpContextAccessor)
        {
            this._config = config;
            this._httpClientFactory = httpClientFactory;
            this._httpContextAccessor = httpContextAccessor;
        }


        public async Task<IList<OrderVm>> GetOrders()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);
            var response = await client.GetAsync(_config["API:Default"] + "/Order");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderVm>>();
        }
        public async Task<IList<OrderDetailVm>> GetOrderDetail(int id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);
            var response = await client.GetAsync(_config["API:Default"] + $"/Order/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderDetailVm>>();
        }

        public async Task<ResultVm<string>> CheckOut(List<int> proID)
        {
            //Send Json body
            var content = new StringContent(JsonConvert.SerializeObject(proID)
                , Encoding.UTF8, "application/json");
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);
            var response = await client.PostAsync(_config["API:Default"] + $"/Order/checkout", content);
            response.EnsureSuccessStatusCode();
            return ResultVm<string>.Success("SuccessFull checkout");
        }
    }
}