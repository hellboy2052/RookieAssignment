using System;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Products
{
    public class Edit
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public ProductFormVm Product { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(MyDbContext context)
            {
                RuleFor(x => x.Product).SetValidator(new ProductValidator(context));
            }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Unit>>
        {
            private readonly MyDbContext _context;
            private readonly IMapper _mapper;
            public Handler(MyDbContext context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResultVm<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Product.Id);

                if (product == null) return null;


                product.Name = request.Product.Name;
                product.Price = request.Product.Price;
                product.Description = request.Product.Description;
                product.Image = request.Product.Image;
                product.BrandId = request.Product.BrandId;
                product.UpdatedDate = DateTime.Now;


                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return ResultVm<Unit>.Failure("Failed to update Product");

                return ResultVm<Unit>.Success(Unit.Value);
            }
        }
    }
}