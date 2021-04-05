using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using ShareVM;

namespace API.Services.Categories
{
    public class Detail
    {
        public class Query : IRequest<CategoryVm>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CategoryVm>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<CategoryVm> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FindAsync(request.Id);

                return new CategoryVm
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
        }
    }
}