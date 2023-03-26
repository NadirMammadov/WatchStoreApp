using IdentityModel.Client;
using WastchStore.Shared.Dtos;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
