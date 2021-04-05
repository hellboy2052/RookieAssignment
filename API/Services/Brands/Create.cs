using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.models;
using MediatR;
using ShareVM;

namespace API.Services.Brands
{
    public class Create
    {
        public class Command : IRequest<Unit>
        {
            public BrandFormVm brandFormVm { get; set; }
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
                var brand = new Brand{
                    Name = request.brandFormVm.Name
                };

                _context.Brands.Add(brand);
                var result = await _context.SaveChangesAsync() > 0;

                if(result) return Unit.Value;

                return Unit.Value;
            }
        }
    }
}