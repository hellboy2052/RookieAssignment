using System.Linq;
using API.Data;
using FluentValidation;
using ShareVM;

namespace API.Services.Categories
{
    public class CategoryValidator : AbstractValidator<CategoryFormVm>
    {
        public CategoryValidator(MyDbContext context)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name)
                .Must(x => context.Categories.FirstOrDefault(c => c.Name == x) == null)
                .WithMessage("Category must be unique");
        }
    }
}