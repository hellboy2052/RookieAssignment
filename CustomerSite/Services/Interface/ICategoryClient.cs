using System.Collections.Generic;
using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services
{
    public interface ICategoryClient
    {
        Task<IList<CategoryVm>> GetCategories();
        
    }
}