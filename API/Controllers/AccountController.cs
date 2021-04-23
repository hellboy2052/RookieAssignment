using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareVM;
using System.Collections.Generic;

namespace API.Controllers
{

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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserVm>> Login(LoginVm loginVm)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginVm.Email);

            if (user == null) return NotFound("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, false);

            if (result.Succeeded)
            {
                var role = await _userManager.GetRolesAsync(user);
                if (role.Count < 0) return BadRequest("Problem with getting user Role");
                return createUserObject(user, role);
            }

            return BadRequest("Incorrect password");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserVm>> Register(RegisterVm registerVm)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerVm.Email))
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerVm.Username))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            var user = new User
            {
                Email = registerVm.Email,
                FullName = registerVm.Fullname,
                UserName = registerVm.Username
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (result.Succeeded)
            {
                var result2 = await _userManager.AddToRoleAsync(user, "member");
            }
            else
            {
                return BadRequest("Problem registering user");
            }

            var role = await _userManager.GetRolesAsync(user);
            if (role.Count > 0) return createUserObject(user, role);

            return BadRequest("Problem registering user");
        }

        [HttpGet]
        public async Task<ActionResult<UserVm>> GetCurrentUser()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
            if (user == null) return NotFound();
            var role = await _userManager.GetRolesAsync(user);
            return createUserObject(user, role);
        }

        [Authorize(Policy = "IsPermitRequire")]
        [HttpGet("List")]
        public async Task<ActionResult<List<AccountVm>>> GetAccounts()
        {
            var accounts = new List<AccountVm>();
            var users = await _userManager.Users.Select(x => x).ToListAsync();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                accounts.Add(createAccountObject(user, role));
            }
            
            if(accounts.Count < 0) return NotFound();
            return accounts;
        }


        private UserVm createUserObject(User user, IList<string> roles)
        {

            return new UserVm
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                FullName = user.FullName,
                Roles = roles
            };
        }
        private AccountVm createAccountObject(User user, IList<string> roles)
        {

            return new AccountVm
            {
                Username = user.UserName,
                Email = user.Email,
                Fullname = user.FullName,
                Roles = roles
            };
        }
    }
}