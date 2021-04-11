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
    public class AccountClient : IAccountClient
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountClient(IHttpClientFactory httpClientFactory, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._httpClientFactory = httpClientFactory;
            this._config = config;
        }

        public async Task<ResultVm<UserVm>> postLogin(LoginVm login)
        {
            //Send Json body
            var content = new StringContent(JsonConvert.SerializeObject(login)
                , Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(_config["API:Default"] + "/api/Account/login", content);
            if(response.IsSuccessStatusCode){
                return ResultVm<UserVm>.Success(await response.Content.ReadAsAsync<UserVm>());
            }
            
            return ResultVm<UserVm>.Failure(response.Content.ReadAsStringAsync().Result.ToString());
            
        }

        public async Task<ResultVm<UserVm>> postRegister(RegisterVm register)
        {
            var content = new StringContent(JsonConvert.SerializeObject(register)
                , Encoding.UTF8, "application/json");
            
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(_config["API:Default"] + "/api/Account/register", content);
            if(response.IsSuccessStatusCode){
                return ResultVm<UserVm>.Success(await response.Content.ReadAsAsync<UserVm>());
            }
            return ResultVm<UserVm>.Failure(response.Content.ReadAsStringAsync().Result.ToString());
        }

        public async Task<ResultVm<UserVm>> getCurrentUser()
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.SetBearerToken(token);
            
            var response = await client.GetAsync(_config["API:Default"] + "/api/Account");
            if(response.IsSuccessStatusCode){
                return ResultVm<UserVm>.Success(await response.Content.ReadAsAsync<UserVm>());
            }
            return ResultVm<UserVm>.Failure(response.Content.ReadAsStringAsync().Result.ToString());
        }

        public async Task<ResultVm<ProfileVm>> getProfile()
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.SetBearerToken(token);
            
            var response = await client.GetAsync(_config["API:Default"] + "/Profile");
            if(response.IsSuccessStatusCode){
                return ResultVm<ProfileVm>.Success(await response.Content.ReadAsAsync<ProfileVm>());
            }
            return ResultVm<ProfileVm>.Failure(response.Content.ReadAsStringAsync().Result.ToString());
        }
    }
}