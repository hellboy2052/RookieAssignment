using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Security
{
    public class IsPermitRequirement : IAuthorizationRequirement
    {

    }

    public class IsPermitRequirementHandler : AuthorizationHandler<IsPermitRequirement>
    {
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public IsPermitRequirementHandler(MyDbContext context,
            IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this._userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsPermitRequirement requirement)
        {
            var userid = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userid == null) return Task.CompletedTask;

            var user = _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userid)
                .Result;

            if (user == null) return Task.CompletedTask;

            var role = _userManager.GetRolesAsync(user).Result;

            if (role.Count <= 0) return Task.CompletedTask;

            foreach (var item in role)
            {
                // Check if user role is superadmin or member
                if (item == "superadmin" || item == "member") context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}