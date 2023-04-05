using WatchStore.Shared.Dtos;
using WatchStoreApp.UI.Models.Auth;

namespace WatchStoreApp.UI.Services
{
    public class SignupService : ISignupService
    {
        private readonly HttpClient _client;

        public SignupService(HttpClient client)
        {
            _client = client;
        }

        public async Task<TResponse<bool>> SignUp(SignUpInput signUpInput)
        {
            var response = await _client.PostAsJsonAsync<SignUpInput>("/api/user/signup", signUpInput);
            return TResponse<bool>.Success(200);
        }
    }
}
