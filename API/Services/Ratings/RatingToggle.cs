using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Ratings
{
    public class RatingToggle
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public int productID { get; set; }
            public double rate { get; set; }
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
                if (request.rate > 5) return ResultVm<Unit>.Failure("Rate point is over 5");

                var product = await _context.Products.FindAsync(request.productID);

                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (product == null) return null;

                var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.productId == request.productID && x.userId == user.Id);
                //Toggle if rating exist or not
                if (rating == null)
                {
                    rating = new Domain.Rating
                    {
                        product = product,
                        user = user,
                        rate = request.rate,
                    };
                    _context.Ratings.Add(rating);
                }
                else
                {
                    rating.rate = request.rate;
                }

                var result = await _context.SaveChangesAsync() > 0;
                if(result) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Something wrong with your rating");
            }
        }

    }
}