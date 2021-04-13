using System.Threading.Tasks;
using CustomerSite.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class OrderList : ViewComponent
    {
        private readonly IOrderClient _orderClient;
        public OrderList(IOrderClient orderClient)
        {
            this._orderClient = orderClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var orders = await _orderClient.GetOrders();
            return View(orders);
        }
    }
}