using System.Threading.Tasks;
using ShareVM;

namespace CustomerSite.Services.Interface
{
    public interface IAccountClient
    {
        Task<ResultVm<UserVm>> postLogin(LoginVm login);

        Task<ResultVm<UserVm>> postRegister(RegisterVm register);

        Task<ResultVm<UserVm>> getCurrentUser();

        Task<ResultVm<ProfileVm>> getProfile();
    }
}