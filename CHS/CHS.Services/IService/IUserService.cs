using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;

namespace CHS.Services.IService
{
    public interface IUserService
    {
        Task<LoginViewModel> LoginAsync(LoginViewModel model);
        Task<UserWithToken> RefreshTokenAsync(RefreshViewModel model);
        Task<RegisterViewModel> RegisterAsync(RegisterViewModel model);
    }
}
