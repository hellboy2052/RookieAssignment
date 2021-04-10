using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using ShareVM;

namespace API.Services.Brands
{
    public class Delete
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public int Id { get; set; }
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
                var brand = await _context.Brands.FindAsync(request.Id);

                if(brand == null) return ResultVm<Unit>.Failure("Failed to delete Brand");
                
                _context.Brands.Remove(brand);

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return ResultVm<Unit>.Failure("Failed to delete Brand");

                return ResultVm<Unit>.Success(Unit.Value);
            }
        }

    }
}