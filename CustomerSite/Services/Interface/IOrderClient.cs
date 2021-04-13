using System.Collections.Generic;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services.Interface
{
    public interface IOrderClient
    {
        Task<IList<OrderVm>> GetOrders();

        Task<IList<OrderDetailVm>> GetOrderDetail(int id);

        Task<ResultVm<string>> CheckOut(List<int>proID);
    }
}