using System.Linq;
using API.Data;
using FluentValidation;
using ShareVM;

namespace API.Services.Brands
{
    public class BrandValidator : AbstractValidator<BrandFormVm>
    {
        public BrandValidator(MyDbContext context)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name)
                .Must(x => context.Brands.FirstOrDefault(c => c.Name == x) == null)
                .WithMessage("Brand must be unique");
        }
    }
}