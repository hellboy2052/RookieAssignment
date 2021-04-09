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
            //check if user currently login or not
            var user = await _accountClient.getCurrentUser();
            if(user != null){
                ViewData["username"] = user.Error == null ? user.Value.Username : string.Empty;
            }
            
            var products = await _productClient.GetProducts();

            if (!string.IsNullOrEmpty(brand)) products = products.Where(x => x.BrandName == brand).Select(x => x).ToList();
            
            if(!string.IsNullOrEmpty(category)) products = products.Select(x => x).Where(x => x.ProductCategories.Any(x => x.Name == category)).ToList();
            
            return View(products);
        }


        public async Task<IActionResult> product(int id)
        {
            var user = await _accountClient.getCurrentUser();
            //check if user currently login or not
            ViewData["username"] = user.Error == null ? user.Value.Username : string.Empty;

            var product = await _productClient.GetProduct(id);
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> rating(int Id, IFormCollection frm){
            var user = await _accountClient.getCurrentUser();
            if(user.Error != null){
               return RedirectToAction(actionName: "login", controllerName: "Account");
            }

            double rate = double.Parse(frm["rating"]);
            return Content("hello there - " + rate + " ,pId: " + Id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
