using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Photo
{
    public class SetMain
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public string Id { get; set; }

            public int proId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Unit>>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<ResultVm<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(p => p.Pictures)
                    .FirstOrDefaultAsync(x => x.Id == request.proId);

                if (product == null) return null;

                var photo = product.Pictures.FirstOrDefault(x => x.Id == request.Id);

                if (photo == null) return null;

                var currentMain = product.Pictures.FirstOrDefault(x => x.IsMain);

                if (currentMain != null) currentMain.IsMain = false;

                photo.IsMain = true;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Problem setting main photo");
            }
        }
    }
}