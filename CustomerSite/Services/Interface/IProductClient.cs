using System.Collections.Generic;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public interface IProductClient
    {
        Task<IList<ProductVm>> GetProducts();

        Task<ProductVm> GetProduct(int id);

        Task<ResultVm<string>> SetRating(int productID, double rate);
        Task<ResultVm<string>> AddToCart(int productID, int quantity = 0);
        Task<ResultVm<string>> DeletFromCart(int productID);
    }
}