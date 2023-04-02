using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WatchStoreApp.UI.Models.Auth;
namespace WatchStoreApp.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;
        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {
            if (!ModelState.IsValid)
            {
                return View(signinInput);
            }
            var response = await _identityService.SignIn(signinInput);
            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(String.Empty, x);

                });
                return View(signinInput);
            }
            return RedirectToAction(nameof(Index), "Home");
        }
        #endregion


        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpInput signupInput)
        {
            if (!ModelState.IsValid)
            {
                return View(signupInput);
            }
            var response = await _identityService.SignUp(signupInput);
            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(String.Empty, x);

                });
                return View(signupInput);
            }
            return RedirectToAction(nameof(SignIn));
        }


        #endregion


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction(nameof(Index), "Home");
        }

    }
}
