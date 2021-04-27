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

namespace API.Services.Brands
{
    public class List
    {
        public class Query : IRequest<ResultVm<List<BrandVm>>> { }

        public class Handler : IRequestHandler<Query, ResultVm<List<BrandVm>>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<List<BrandVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var brands = await _context.Brands
                    .ToListAsync();
                return ResultVm<List<BrandVm>>.Success(_mapper.Map<List<Domain.Brand>, List<BrandVm>>(brands));
            }
        }
    }
}