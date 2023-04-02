using WatchStore.Shared.Dtos;
using WatchStoreApp.UI.Models.Auth;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<TResponse<bool>> SignIn(SigninInput signInInput);
        Task<TResponse<bool>> SignUp(SignUpInput signUpInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
