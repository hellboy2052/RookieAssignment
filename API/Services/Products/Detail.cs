using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Products
{
    public class Detail
    {
        public class Query : IRequest<ResultVm<ProductVm>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResultVm<ProductVm>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(MyDbContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<ProductVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .ProjectTo<ProductVm>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product == null) return ResultVm<ProductVm>.Failure("Product doesnt exist!");

                return ResultVm<ProductVm>.Success(product);
            }
        }
    }
}