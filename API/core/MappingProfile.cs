using API.models;
using AutoMapper;
using ShareVM;

namespace API.core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, Product>();
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.BrandName, o => 
                    o.MapFrom(s => s.Brand.Name));
            CreateMap<CategoryProduct, CategoryVm>()
                .ForMember(c => c.Id, o => o.MapFrom(s => s.Category.Id))
                .ForMember(c => c.Name, o => o.MapFrom(s => s.Category.Name));
        }
    }
}