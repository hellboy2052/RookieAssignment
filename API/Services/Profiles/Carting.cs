using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;
using Domain;

namespace API.Services.Profiles
{
    public class Carting
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public int productId { get; set; }

            public int quantity { get; set; }
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

                var increase = request.quantity;

                if(request.quantity == 0) increase = 1;

                if (cartItem == null)
                {
                    

                    cartItem = new CartItem
                    {
                        Product = product,
                        User = user,
                        quantity = increase,
                    };
                    _context.Add(cartItem);
                }
                else
                {
                    cartItem.quantity = cartItem.quantity + increase;

                }

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Something wrong with your Cart");
            }
        }
    }
}