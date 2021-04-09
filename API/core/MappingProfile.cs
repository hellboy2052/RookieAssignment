using Domain;
using AutoMapper;
using ShareVM;
using System.Linq;

namespace API.core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Rating
            CreateMap<Rating, RatingVm>();
            //Category
            CreateMap<Category, CategoryVm>();
            //Brand
            CreateMap<Brand, BrandVm>();
            //Product
            CreateMap<Product, Product>();
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.BrandName, o => 
                    o.MapFrom(s => s.Brand.Name))
                .ForMember(p => p.ratingCount, o => o.MapFrom(s => s.rate.Count))
                .ForMember(p => p.rating, o => o.MapFrom(s => s.rate.Select(x => x.rate).Sum()));

            CreateMap<CategoryProduct, CategoryVm>()
                .ForMember(c => c.Id, o => o.MapFrom(s => s.Category.Id))
                .ForMember(c => c.Name, o => o.MapFrom(s => s.Category.Name));
        }
    }
}