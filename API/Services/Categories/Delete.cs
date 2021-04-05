using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;

namespace API.Services.Categories
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
                var category = await _context.Categories.FindAsync(request.Id);

                _context.Remove(category);

                var result = await _context.SaveChangesAsync() > 0;

                if(result) return Unit.Value;

                return Unit.Value;
            }
        }
    }
}