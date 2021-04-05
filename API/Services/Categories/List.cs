using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Categories
{
    public class List
    {
        public class Query : IRequest<List<CategoryVm>>
        {

        }

        public class Handler : IRequestHandler<Query, List<CategoryVm>>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<List<CategoryVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Categories.Select(x => new CategoryVm
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            }
        }
    }
}