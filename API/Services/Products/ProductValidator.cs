using System.Linq;
using API.Data;
using FluentValidation;
using ShareVM;

namespace API.Services.Products
{
    public class ProductValidator : AbstractValidator<ProductFormVm>
    {
        public ProductValidator(MyDbContext context)
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.BrandId)
                .Must(x => context.Brands.FirstOrDefault(b => b.Id == x) != null)
                .WithMessage("Invalid brand");
            
            RuleFor(x => x.CategoryName).NotEmpty().NotNull();
            RuleForEach(x => x.CategoryName)
                .Must(x => context.Categories.FirstOrDefault(b => b.Name == x) != null)
                .WithMessage("Invalid Category");
        }
    }
}