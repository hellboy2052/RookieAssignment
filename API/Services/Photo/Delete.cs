using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Photo
{
    // Delete one photo and not the main one
    public class Delete
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public string Id { get; set; }

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

                if (product == null) return null;

                var picture = product.Pictures.FirstOrDefault(x => x.Id == request.Id);

                if (picture == null) return null;

                if (picture.IsMain) return ResultVm<Unit>.Failure("You cannot delete your main picture");

                var result = await _photoAccessor.DeletePhoto(picture.Id);

                if (result == null) return ResultVm<Unit>.Failure("Problem deleting picture from Cloudinary");

                product.Pictures.Remove(picture);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Problem deleting picture from API");
            }
        }
    }
}