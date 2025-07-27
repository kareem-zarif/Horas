namespace Horas.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductResDto, Product>().ReverseMap();
                //.ForMember(dest => dest.Rating, opt =>
                //    opt.MapFrom(src =>
                //    src.Reviews != null && src.Reviews.Any()
                //        ? src.Reviews.Sum(x => x.Rating) / (src.Reviews.Count * 5.0)
                //        : 0))
                //.ForMember(dest => dest.Rating, opt =>
                //    opt.MapFrom(src => src.Suppliers.Select(x => x.FactoryName)));
            #endregion


            #region SubCategory
            CreateMap<SubCategoryCreateDto, SubCategory>().ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<SubCategoryUpdateDto, SubCategory>().ReverseMap();
            CreateMap<SubCategoryResDto, SubCategory>().ReverseMap();
            #endregion

            #region Wishlist

            CreateMap<Wishlist, WishListCreateDto>()
                .ReverseMap();
            CreateMap<Wishlist, UpdateWishlistDto>()
                .ReverseMap();
            CreateMap<Wishlist, WishListResDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.UserName))
                .ForMember(dest => dest.ProductWishlist,opt => opt.MapFrom(src => src.ProductWishLists))
                .ReverseMap();

            #endregion
           
            #region ProductWishlist
            CreateMap<ProductWishList, ProductWishlistCreateDto>().ReverseMap();
            CreateMap<ProductWishlistUpdateDto, ProductWishList>().ReverseMap();
            CreateMap<ProductWishList,ProductWishlistResDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();

            #endregion

            #region Orders
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>().ReverseMap();
            CreateMap<Order, OrderResDto>()
                       .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.UserName : null))
                       .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod != null ? (int?)src.PaymentMethod.PaymentType : null))
                       .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)).
                        ReverseMap(); 
            #endregion

            #region OrderItem

            CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemUpdateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemResDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)).ReverseMap();
            #endregion

            #region OrderStatusHistory
            CreateMap<OrderStatusHistoryCreateDto, OrderStatusHistory>()
                     .ReverseMap();
            CreateMap<OrderStatusHistoryUpdateDto, OrderStatusHistory>()
                    .ReverseMap();
            CreateMap<OrderStatusHistory, OrderStatusHistoryResDto>()
                     .ReverseMap();

            #endregion
            #region PaymentMethod

            CreateMap<PaymentMethodCreateDto, PaymentMethod>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodUpdateDto>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodResDto>()
                .ForMember(dest => dest.CustomerName, opt =>opt.MapFrom(src=>src.Customer.UserName))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                .ReverseMap();
            #endregion
        }
    }
}
