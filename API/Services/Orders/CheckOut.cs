using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Orders
{
    public class CheckOut
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public List<int> productIds { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Unit>>
        {
            private readonly MyDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(MyDbContext context, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._context = context;
            }

            public async Task<ResultVm<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                // Add order
                Order order = new Order
                {
                    User = user,
                };
                _context.Orders.Add(order);
                var result1 = await _context.SaveChangesAsync() > 0;

                if (!result1) return ResultVm<Unit>.Failure("#Checkout: Problem with result 1 -> adding new Order");

                // Add list of order detail
                foreach (var proID in request.productIds)
                {
                    var product = await _context.Products.FindAsync(proID);

                    if (product == null) return null;

                    var cart = await _context.CartItems.FirstOrDefaultAsync(x => x.productId == product.Id && x.userId == user.Id);

                    if (cart == null) return null;

                    OrderDetail orderDetail = new OrderDetail
                    {
                        orderId = 1,
                        Order = order,
                        Product = product,
                        price = product.Price,
                        quantity = cart.quantity,
                        totalPrice = product.Price * cart.quantity
                    };
                    order.orders.Add(orderDetail);

                    _context.CartItems.Remove(cart);

                }

                var result2 = await _context.SaveChangesAsync() > 0;

                if (result2) return ResultVm<Unit>.Success(Unit.Value);

                return ResultVm<Unit>.Failure("Problem with checkout");
            }
        }
    }
}