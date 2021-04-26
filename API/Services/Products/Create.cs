using System;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Products
{
    public class Create
    {
        public class Command : IRequest<ResultVm<int>>
        {
            public ProductFormVm product { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(MyDbContext context)
            {
                RuleFor(x => x.product).SetValidator(new ProductValidator(context));
            }
        }

        public class Handler : IRequestHandler<Command, ResultVm<int>>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<ResultVm<int>> Handle(Command request, CancellationToken cancellationToken)
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

                    product.ProductCategories.Add(
                        new CategoryProduct
                        {
                            Category = category,
                            Product = product
                        }
                    );
                }

                _context.Add(product);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return ResultVm<int>.Failure("Failed to create Product");

                return ResultVm<int>.Success(product.Id);
            }
        }
    }
}