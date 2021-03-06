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
    public class Detail
    {
        public class Query : IRequest<ResultVm<BrandVm>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResultVm<BrandVm>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<BrandVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var brand = await _context.Brands
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return ResultVm<BrandVm>.Success(_mapper.Map<Domain.Brand, BrandVm>(brand));
            }
        }
    }
}