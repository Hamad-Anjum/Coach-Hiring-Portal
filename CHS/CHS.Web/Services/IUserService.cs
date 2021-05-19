using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;

namespace CHS.Web.Services
{
    public interface IUserService
    {
        Task<LoginViewModel> LoginAsync(LoginViewModel model);
        Task<UserWithToken> RefreshTokenAsync(RefreshViewModel model);
        Task<RegisterViewModel> RegisterAsync(RegisterViewModel model);
    }
}
