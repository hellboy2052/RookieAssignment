using System.Collections.Generic;
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
    public class List
    {
        public class Query : IRequest<List<ProductVm>>
        {

        }

        public class Handler : IRequestHandler<Query, List<ProductVm>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<List<ProductVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await _context.Products
                    .ProjectTo<ProductVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return products;
            }
        }
    }
}