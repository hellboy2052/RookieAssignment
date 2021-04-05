using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserVm>> Login(LoginVm loginVm)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginVm.Email);

            if (user == null) return NotFound();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, false);

            if (result.Succeeded) return createUserObject(user);

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserVm>> Register(RegisterVm registerVm)
        {
            if(await _userManager.Users.AnyAsync(x => x.Email == registerVm.Email)){
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }
            if(await _userManager.Users.AnyAsync(x => x.UserName == registerVm.Username)){
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            var user = new User{
                Email = registerVm.Email,
                FullName = registerVm.Fullname,
                UserName = registerVm.Username
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if(result.Succeeded) return createUserObject(user);

            return BadRequest("Problem registering user");

        }

        [HttpGet]
        public async Task<ActionResult<UserVm>> GetCurrentUser()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
            if(user == null) return NotFound();
            return createUserObject(user);
        }


        private UserVm createUserObject(User user)
        {
            return new UserVm
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                FullName = user.FullName
            };
        }
    }
}