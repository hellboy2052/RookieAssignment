using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Brands
{
    public class List
    {
        public class Query : IRequest<List<BrandVm>> { }

        public class Handler : IRequestHandler<Query, List<BrandVm>>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<List<BrandVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Brands
                    .Select(x => new BrandVm { Id = x.Id, Name = x.Name })
                    .ToListAsync();
            }
        }
    }
}