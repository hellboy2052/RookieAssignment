using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Categories
{
    public class List
    {
        public class Query : IRequest<ResultVm<List<CategoryVm>>>
        {

        }

        public class Handler : IRequestHandler<Query, ResultVm<List<CategoryVm>>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<List<CategoryVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories
                    .ProjectTo<CategoryVm>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return ResultVm<List<CategoryVm>>.Success(categories);
            }
        }
    }
}