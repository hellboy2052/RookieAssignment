using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using ShareVM;

namespace API.Services.Categories
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
                var category = await _context.Categories.FindAsync(request.Id);

                if(category == null) return ResultVm<Unit>.Failure("Failed to find category \r\nIn deleting Process");

                _context.Remove(category);

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return ResultVm<Unit>.Failure("Failed to delete category");

                return ResultVm<Unit>.Success(Unit.Value);
            }
        }
    }
}