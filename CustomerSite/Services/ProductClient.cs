using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShareVM;

namespace CustomerSite.Services
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductClient(IHttpClientFactory httpClientFactory, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IList<ProductVm>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_config["API:Default"] + "/Products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
        public async Task<ProductVm> GetProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.SetBearerToken(token);
            
            var response = await client.GetAsync(_config["API:Default"] + $"/Products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<ResultVm<string>> SetRating(int productID, double rate)
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.SetBearerToken(token);

            var response = await client.PostAsync(_config["API:Default"] + $"/Rate/{productID}?rate={rate}", null);

            if(response.IsSuccessStatusCode){
                return ResultVm<string>.Success("complete");
            }
            return ResultVm<string>.Failure("failed to rate a product");
        }

        public async Task<ResultVm<string>> AddToCart(int productID, int quantity = 0)
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.SetBearerToken(token);

            var response = await client.PostAsync(_config["API:Default"] + $"/Profile/AddToCart/{productID}?quantity={quantity}", null);

            if(response.IsSuccessStatusCode){
                return ResultVm<string>.Success("complete");
            }
            return ResultVm<string>.Failure("failed to add product to cart");
        }

        public async Task<ResultVm<string>> DeletFromCart(int productID)
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.SetBearerToken(token);

            var response = await client.DeleteAsync(_config["API:Default"] + $"/Profile/DeleteFromCart/{productID}");

            if(response.IsSuccessStatusCode){
                return ResultVm<string>.Success("complete");
            }
            return ResultVm<string>.Failure("failed to add product to cart");
        }
    }
}