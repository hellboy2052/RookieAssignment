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

namespace API.Services.Orders
{
    public class Detail
    {
        public class Query : IRequest<ResultVm<List<OrderDetailVm>>>
        {
            public int orderId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResultVm<List<OrderDetailVm>>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<List<OrderDetailVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orderDetail = await _context.OrderDetails
                    .Where(x => x.orderId == request.orderId)
                    .ProjectTo<OrderDetailVm>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                
                return ResultVm<List<OrderDetailVm>>.Success(orderDetail);
            }
        }
    }
}