using AutoMapper;
using Horas.Api.Dtos.Address;
using Horas.Api.Dtos.Cart;
using Horas.Api.Dtos.CartItem;
using Horas.Api.Dtos.Product;
using Horas.Api.Dtos.ProductSupplier;
using Horas.Api.Dtos.SubCategory;
using Horas.Api.Dtos.Supplier;
using Horas.Domain;
using Horas.Domain.Entities;

namespace Horas.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Product
            CreateMap<ProductCreateDto, Product>().ReverseMap();

            CreateMap<ProductUpdateDto, Product>().ReverseMap();

            CreateMap<Product, ProductResDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Reviews != null && src.Reviews.Any()
                    ? src.Reviews.Sum(x => x.Rating) / (src.Reviews.Count) : 0))
                .ReverseMap();
            #endregion


            #region SubCategory
            CreateMap<SubCategoryCreateDto, SubCategory>().ReverseMap();

            CreateMap<SubCategoryUpdateDto, SubCategory>().ReverseMap();

            CreateMap<SubCategory, SubCategoryResDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            #endregion


            #region Cart
            CreateMap<CartCreateDto, Cart>().ReverseMap();

            CreateMap<CartUpdateDto, Cart>().ReverseMap();

            CreateMap<Cart, CartResDto>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
                .ReverseMap();
                //.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Product.PricePerPiece * ci.Quantity)))
                //.ForMember(dest => dest.CartItems.Sum(ci => ci.Quantity), opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Quantity)));
            #endregion


            #region CartItem
            CreateMap<CartItemCreateDto, CartItem>()
                .ForPath(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ReverseMap();

            CreateMap<CartItemUpdateDto, CartItem>()
                .ForPath(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ReverseMap();

            CreateMap<CartItem, CartItemResDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.PricePerPiece, opt => opt.MapFrom(src => src.Product.PricePerPiece))
                .ForMember(dest => dest.PricePer100Piece, opt => opt.MapFrom(src => src.Product.PricePer100Piece))
                .ForMember(dest => dest.ProductPicsPathes, opt => opt.MapFrom(src => src.Product.ProductPicsPathes))
                .ReverseMap();
            #endregion


            #region Supplier
            CreateMap<SupplierCreateDto, Supplier>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ReverseMap();

            CreateMap<SupplierUpdateDto, Supplier>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ReverseMap();

            CreateMap<Supplier, SupplierResDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault().State))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault().City))
                .ReverseMap();
            #endregion


            #region Address
            CreateMap<AddressCreateDto, Address>()
                .ReverseMap();

            CreateMap<AddressUpdateDto, Address>()
                .ReverseMap();

            CreateMap<Address, AddressResDto>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.Id))d
                //.ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))d
                .ReverseMap();
            #endregion


            #region ProductSupplier
            CreateMap<ProductSupplierCreateDto, ProductSupplier>()
                .ReverseMap();

            CreateMap<ProductSupplierUpdateDto, ProductSupplier>()
                .ReverseMap();

            CreateMap<ProductSupplier, ProductSupplierResDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.PricePerPiece, opt => opt.MapFrom(src => src.Product.PricePerPiece))
                .ForMember(dest => dest.FactoryName, opt => opt.MapFrom(src => src.Supplier.FactoryName))
                .ReverseMap();
            #endregion
            
        }
    }
}
