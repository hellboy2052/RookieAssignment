using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services;
using CustomerSite.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClient _productClient;
        private readonly IAccountClient _accountClient;

        public HomeController(ILogger<HomeController> logger, IProductClient productClient, IAccountClient accountClient)
        {
            this._accountClient = accountClient;
            _logger = logger;
            _productClient = productClient;
        }

        public async Task<IActionResult> Index(string brand, string category)
        {
            var user = await _accountClient.getProfile();
            //check if user currently login or not
            ViewData["username"] = user.Error == null ? user.Value.Username : string.Empty;
            ViewData["cart"] = user.Error == null ? user.Value.Cart.Count : null;

            var products = await _productClient.GetProducts();

            if (!string.IsNullOrEmpty(brand)) products = products.Where(x => x.BrandName == brand).Select(x => x).ToList();
            
            if(!string.IsNullOrEmpty(category)) products = products.Select(x => x).Where(x => x.ProductCategories.Any(x => x.Name == category)).ToList();
            
            return View(products);
        }


        public async Task<IActionResult> product(int id)
        {
            var user = await _accountClient.getProfile();
            //check if user currently login or not
            ViewData["username"] = user.Error == null ? user.Value.Username : string.Empty;
            ViewData["cart"] = user.Error == null ? user.Value.Cart.Count : null;
            var product = await _productClient.GetProduct(id);
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity = 0){
            var user = await _accountClient.getProfile();
            if(user.Error != null) {
                return RedirectToAction(actionName: "login", controllerName: "Account");
            }
            
            
            var result = await _productClient.AddToCart(id, quantity);
            if(result.Error != null){
                return Redirect("~/");
            }
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int id){
            var user = await _accountClient.getProfile();
            if(user.Error != null) {
                return RedirectToAction(actionName: "login", controllerName: "Account");
            }
            var result = await _productClient.DeletFromCart(id);
            if(result.Error != null){
                return Redirect("~/");
            }

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }


        [HttpPost]
        public async Task<IActionResult> rating(int Id, IFormCollection frm){
            var user = await _accountClient.getProfile();
            if(user.Error != null){
               return RedirectToAction(actionName: "login", controllerName: "Account");
            }

            // Get value from checked radio
            double rate = double.Parse(frm["rating"]);
            var result =await _productClient.SetRating(Id, rate);
            if(result.Error != null){
                return Redirect("~/");
            }
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
