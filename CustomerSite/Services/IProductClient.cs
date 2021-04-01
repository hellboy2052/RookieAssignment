using System.Collections.Generic;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public interface IProductClient
    {
        Task<IList<ProductVm>> GetProducts();

        Task<ProductVm> GetProduct(int id);

        Task<IList<ProductVm>> GetProductByCategory(string n);
    }
}