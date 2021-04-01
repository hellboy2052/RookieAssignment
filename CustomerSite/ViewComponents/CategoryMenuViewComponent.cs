using System.Threading.Tasks;
using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryClient _categoryClient;

        public CategoryMenuViewComponent(ICategoryClient categoryClient)
        {
            this._categoryClient = categoryClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryClient.GetCategories();
            return View(categories);
        }
    }
}