using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;

namespace API.Services.Products
{
    public class Delete
    {
        public class Command : IRequest<Unit>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var Product = await _context.Products.FindAsync(request.Id);

                if (Product == null) return Unit.Value;

                _context.Products.Remove(Product);

                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}