using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using ShareVM;

namespace API.Services.Products
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
                var Product = await _context.Products.FindAsync(request.Id);

                // if (Product == null) return ResultVm<Unit>.Failure("Failed to find a product \r\nIn Deleting process");

                _context.Products.Remove(Product);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return ResultVm<Unit>.Failure("Failed to delete a product");
                return ResultVm<Unit>.Success(Unit.Value);
            }
        }
    }
}