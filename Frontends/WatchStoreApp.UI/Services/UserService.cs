using WatchStoreApp.UI.Models.User;

namespace WatchStoreApp.UI.Services;
public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }
    public async Task<UserUsernameViewModel> GetUserName(string userId)
    {
        var response = await _client.GetFromJsonAsync<UserUsernameViewModel>($"/api/user/getusername/{userId}");
        return response;
    }
    public async Task<UserUserIdViewModel> GetUserById(string userName)
    {
        var response = await _client.GetFromJsonAsync<UserUserIdViewModel>($"/api/user/GetUserId/{userName}");
        return response;
    }
    public async Task<UserViewModel> GetUser()
    {
        return await _client.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
    }
    public async Task<List<UserViewModel>> GetUsers()
    {
        return await _client.GetFromJsonAsync<List<UserViewModel>>("/api/user/getusers");
    }
}
