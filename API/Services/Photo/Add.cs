using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Photo
{
    public class Add
    {
        public class Command : IRequest<ResultVm<Picture>>
        {
            public IFormFile File { get; set; }
            public int productId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Picture>>
        {
            private readonly MyDbContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(MyDbContext context, IPhotoAccessor photoAccessor)
            {
                this._photoAccessor = photoAccessor;
                this._context = context;
            }

            public async Task<ResultVm<Picture>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(p => p.Pictures)
                    .FirstOrDefaultAsync(x => x.Id == request.productId);
                
                if(product == null) return null;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new Picture{
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if(!product.Pictures.Any(x => x.IsMain)) photo.IsMain = true;

                product.Pictures.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;

                if(result) return ResultVm<Picture>.Success(photo);

                return ResultVm<Picture>.Failure("Problem with adding Picture");
            }
        }
    }
}