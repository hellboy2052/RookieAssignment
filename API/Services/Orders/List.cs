using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Orders
{
    public class List
    {
        public class Query : IRequest<ResultVm<List<OrderVm>>>
        {

        }

        public class Handler : IRequestHandler<Query, ResultVm<List<OrderVm>>>
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

            public async Task<ResultVm<List<OrderVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders
                    .Where(x => x.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<OrderVm>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                
                return ResultVm<List<OrderVm>>.Success(orders);
            }
        }
    }
}