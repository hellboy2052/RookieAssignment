using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CustomerSite.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareVM;

namespace CustomerSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClient _accountClient;
        public AccountController(IAccountClient accountClient)
        {
            this._accountClient = accountClient;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _accountClient.getCurrentUser();
            if (user.Error == null)
            {
                return View(user);
            }
            return Redirect("~/account/login");

        }

        [HttpGet]
        public async Task<IActionResult> login()
        {
            var user = await _accountClient.getCurrentUser();
            if (user.Error == null)
            {
                return Redirect("~/");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountClient.postLogin(loginVm);
                if (user.Error != null)
                {
                    ModelState.AddModelError(string.Empty, user.Error);
                    return View(loginVm);
                }

                if (user.Value.Token != null)
                {
                    Response.Cookies.Append("jwt", user.Value.Token, new CookieOptions { Expires = DateTime.Now.AddDays(2d) });
                    return Redirect("~/");
                }
                ModelState.AddModelError(string.Empty, user.Error);
                return View(loginVm);
            }

            return View(loginVm);
        }

        public async Task<IActionResult> register(){
            var user = await _accountClient.getCurrentUser();
            if (user.Error == null)
            {
                return Redirect("~/");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> register(RegisterVm registerVm){
            if (ModelState.IsValid)
            {
                var user = await _accountClient.postRegister(registerVm);
                if (user.Error != null)
                {
                    ModelState.AddModelError(string.Empty, user.Error);
                    return View(registerVm);
                }

                if (user.Value.Token != null)
                {   
                    Response.Cookies.Append("jwt", user.Value.Token, new CookieOptions { Expires = DateTime.Now.AddDays(2d) });
                    return Redirect("~/");
                }
                ModelState.AddModelError(string.Empty, user.Error);
                return View(registerVm);
            }
            return View(registerVm);
        }

        public async Task<IActionResult> logout()
        {
            var user = await _accountClient.getCurrentUser();
            if (Request.Cookies["jwt"] != null)
            {
                Response.Cookies.Delete("jwt", new CookieOptions { Expires = DateTime.Now.AddDays(-1d) });
            }
            return Redirect("~/");
        }
    }
}