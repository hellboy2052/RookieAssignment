using System.Collections.Generic;
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
    public class List
    {
        public class Query : IRequest<ResultVm<List<ProductVm>>>
        {

        }

        public class Handler : IRequestHandler<Query, ResultVm<List<ProductVm>>>
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

            public async Task<ResultVm<List<ProductVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await _context.Products
                    .ProjectTo<ProductVm>(_mapper.ConfigurationProvider, new {currentUsername = _userAccessor.GetUsername()})
                    .ToListAsync(cancellationToken);

                return ResultVm<List<ProductVm>>.Success(products);
            }
        }
    }
}