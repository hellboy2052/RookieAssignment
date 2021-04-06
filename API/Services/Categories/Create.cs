using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.models;
using MediatR;
using ShareVM;

namespace API.Services.Categories
{
    public class Create
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public CategoryFormVm categoryFormVm { get; set; }
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
                var Category = new Category
                {
                    Name = request.categoryFormVm.Name
                };

                _context.Categories.Add(Category);

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return ResultVm<Unit>.Failure("Failed to create category");

                return ResultVm<Unit>.Success(Unit.Value);

            }
        }
    }
}