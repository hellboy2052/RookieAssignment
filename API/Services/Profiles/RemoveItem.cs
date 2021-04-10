using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Profiles
{
    public class RemoveItem
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public int productId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Unit>>
        {
            private readonly MyDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(MyDbContext context, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._context = context;
            }

            public async Task<ResultVm<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.productId);

                if (product == null) return null;

                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                var cartItem = await _context.CartItems
                    .FirstOrDefaultAsync(x => x.productId == request.productId && x.userId == user.Id);

                if(cartItem == null) return null;

                _context.CartItems.Remove(cartItem);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Problem removing item from cart");
            }
        }
    }
}