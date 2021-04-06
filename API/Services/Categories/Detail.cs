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
    public class Detail
    {
        public class Query : IRequest<ResultVm<CategoryVm>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResultVm<CategoryVm>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<CategoryVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories
                    .ProjectTo<CategoryVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if(category == null) return ResultVm<CategoryVm>.Failure("Category not found!");

                return ResultVm<CategoryVm>.Success(category);
            }
        }
    }
}