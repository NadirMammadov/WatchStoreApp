using WatchStore.Shared.Dtos;
using WatchStoreApp.UI.Models.Auth;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface ISignupService
    {
        Task<TResponse<bool>> SignUp(SignUpInput signUpInput);
    }
}
