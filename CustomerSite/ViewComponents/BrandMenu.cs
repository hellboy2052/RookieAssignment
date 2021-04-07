using System.Threading.Tasks;
using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class BrandMenu : ViewComponent
    {
        private readonly IBrandClient _brandClient;
        public BrandMenu(IBrandClient brandClient)
        {
            this._brandClient = brandClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var brands = await _brandClient.GetBrands();
            return View(brands);
        }
    }
}