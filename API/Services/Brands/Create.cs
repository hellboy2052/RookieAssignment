using System.Threading;
using System.Threading.Tasks;
using API.Data;
using Domain;
using FluentValidation;
using MediatR;
using ShareVM;

namespace API.Services.Brands
{
    public class Create
    {
        public class Command : IRequest<ResultVm<Unit>>
        {
            public BrandFormVm brandFormVm { get; set; }
        }

        public class ComandValidator : AbstractValidator<Command>
        {
            public ComandValidator(MyDbContext context)
            {
                RuleFor(x => x.brandFormVm).SetValidator(new BrandValidator(context));
            }
        }

        public class Handler : IRequestHandler<Command, ResultVm<Unit>>
        {
            private readonly MyDbContext _context;
            public Handler(MyDbContext context)
            {
                this._context = context;
            }

            public async Task<ResultVm<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var brand = new Brand{
                    Name = request.brandFormVm.Name
                };

                _context.Brands.Add(brand);
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return ResultVm<Unit>.Failure("Failed to create brand");

                return ResultVm<Unit>.Success(Unit.Value);
            }
        }
    }
}