using System.Threading.Tasks;
using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class BrandMenuViewComponent : ViewComponent
    {
        private readonly IBrandClient _brandClient;

        public BrandMenuViewComponent(IBrandClient brandClient)
        {
            _brandClient = brandClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var brands = await _brandClient.GetBrands();
            return View(brands);
        }
    }
}