﻿namespace WatchStoreApp.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Profile()
        {
            return View(await _userService.GetUser());
        }
    }
}
