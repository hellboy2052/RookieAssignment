using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Photo
{
    public class DeleteAll
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public int proId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Unit>>
        {
            private readonly MyDbContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(MyDbContext context, IPhotoAccessor photoAccessor)
            {
                this._photoAccessor = photoAccessor;
                this._context = context;
            }

            public async Task<ResultVm<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(p => p.Pictures)
                    .FirstOrDefaultAsync(x => x.Id == request.proId);

                if(product.Pictures == null || product.Pictures.Count < 1){
                    return ResultVm<Unit>.Success(Unit.Value);
                }
                foreach (var item in product.Pictures)
                {
                    var result = await _photoAccessor.DeletePhoto(item.Id);

                    if (result == null) return ResultVm<Unit>.Failure("Problem deleting picture from Cloudinary");

                    product.Pictures.Remove(item);

                }

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Problem deleting all picture from API");
            }
        }
    }
}