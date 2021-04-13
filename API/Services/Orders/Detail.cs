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
        public class Query : IRequest<ResultVm<OrderDetailVm>>
        {
            public int orderId { get; set; }

            public int productId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResultVm<OrderDetailVm>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<OrderDetailVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orderDetail = await _context.OrderDetails
                    .ProjectTo<OrderDetailVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.orderId == request.orderId && x.productId == request.productId);
                
                return ResultVm<OrderDetailVm>.Success(orderDetail);
            }
        }
    }
}