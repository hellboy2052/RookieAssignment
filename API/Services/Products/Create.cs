using System;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Products
{
    public class Create
    {
        public class Command : IRequest<Unit>
        {
            public ProductFormVm product { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Name = request.product.Name,
                    Price = request.product.Price,
                    Description = request.product.Description,
                    Image = request.product.Image,
                    BrandId = request.product.BrandId,
                    CreatedDate = DateTime.Now
                };

                foreach (var cate in request.product.CategoryName)
                {
                    var category = await _context.Categories
                        .FirstOrDefaultAsync(x => x.Name == cate);

                    if (category == null) return Unit.Value;

                    product.ProductCategories.Add(
                        new CategoryProduct
                        {
                            Category = category,
                            Product = product
                        }
                    );
                }

                _context.Add(product);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}