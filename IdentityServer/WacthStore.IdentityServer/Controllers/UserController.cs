using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System.Threading.Tasks;
using WacthStore.IdentityServer.Dtos;
using WacthStore.IdentityServer.Models;
using WastchStore.Shared.Dtos;
using static IdentityServer4.IdentityServerConstants;

namespace WacthStore.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser()
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
            };
            var result = await _userManager.CreateAsync(user, signupDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(e => e.Description).ToList(), 400));
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return BadRequest();
            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email });

        }
    }
}
