using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using ShareVM;

namespace API.Services.Brands
{
    public class Detail
    {
        public class Query : IRequest<BrandVm>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, BrandVm>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<BrandVm> Handle(Query request, CancellationToken cancellationToken)
            {
                var brand = await _context.Brands.FindAsync(request.Id);
                return new BrandVm { Id = brand.Id ,Name = brand.Name };
            }
        }
    }
}