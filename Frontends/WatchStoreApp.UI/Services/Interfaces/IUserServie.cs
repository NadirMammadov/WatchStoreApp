﻿using WatchStoreApp.UI.Models.User;

namespace WatchStoreApp.UI.Services.Interfaces;
public interface IUserService
{
    Task<UserViewModel> GetUser();
    Task<List<UserViewModel>> GetUsers();
    Task<UserUserIdViewModel> GetUserById(string userName);
    Task<UserUsernameViewModel> GetUserName(string userId);
}
