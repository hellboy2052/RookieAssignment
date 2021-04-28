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
            string currentUsername = null;
            // Cart
            CreateMap<CartItem, CartItemVm>();
            //Rating
            CreateMap<Rate, RateVm>();
            //Category
            CreateMap<Category, CategoryVm>();
            CreateMap<CategoryProduct, CategoryVm>()
                .ForMember(c => c.Id, o => o.MapFrom(s => s.Category.Id))
                .ForMember(c => c.Name, o => o.MapFrom(s => s.Category.Name));
            //Brand
            CreateMap<Brand, BrandVm>();
            //Product
            CreateMap<Product, Product>();
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.BrandName, o =>
                    o.MapFrom(s => s.Brand.Name))
                .ForMember(p => p.ratingCount, o => o.MapFrom(s => s.rate.Count))
                .ForMember(p => p.rating, o => o.MapFrom(s => s.rate.Select(x => x.rate).Sum()))
                .ForMember(p => p.IsRate,
                    o => o.MapFrom(s => s.rate.Any(x => x.user.UserName == currentUsername)))
                .ForMember(p => p.currentRate,
                    o => o.MapFrom(s => s.rate.FirstOrDefault(x => x.user.UserName == currentUsername).rate))
                .ForMember(p => p.Image, o => o.MapFrom(s => s.Pictures.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(p => p.Images, o => o.MapFrom(s => s.Pictures.Select(x => x.Url)));
            // Profile
            CreateMap<User, ProfileVm>();
            // Order
            CreateMap<Order, OrderVm>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.Fullname, o => o.MapFrom(s => s.User.FullName));
            CreateMap<OrderDetail, OrderDetailVm>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Product.Pictures.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Product.Brand.Name))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Product.Name));
            // Picture
            CreateMap<Picture, PictureVm>();




        }
    }
}