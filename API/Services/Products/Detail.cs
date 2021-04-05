using System.Threading;
using System.Threading.Tasks;
using API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Products
{
    public class Detail
    {
        public class Query : IRequest<ProductVm>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProductVm>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ProductVm> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .ProjectTo<ProductVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                if(product == null) return null;

                return product;
            }
        }
    }
}