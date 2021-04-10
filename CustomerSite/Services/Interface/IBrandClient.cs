using System.Collections.Generic;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public interface IBrandClient
    {
        Task<IList<BrandVm>> GetBrands(); 
    }
}