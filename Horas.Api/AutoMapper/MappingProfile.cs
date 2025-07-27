

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

            #region Category

            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<CategoryResDto, Category>().ReverseMap();



            #endregion

            #region Customer

            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
            CreateMap<CustomerReadDto, Customer>().ReverseMap();
            CreateMap<Customer, CustomerResDto>()
            .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.Orders != null ? src.Orders.Count : 0))
.ReverseMap();
            //CreateMap<CustomerResDto, Customer>()
            //.ForMember(dest => dest.Orders.Count, opt => opt.MapFrom(src => src.OrdersCount)).ReverseMap();
            // ForMember(dest => dest.MessagesCount, opt => opt.MapFrom(src => src.Messages.Count)).ReverseMap();
            #endregion
            #region order
            CreateMap<Order, OrderReadDto>().ReverseMap();
            // CreateMap<OrderItem,OrderItemReadDto>()
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)).ReverseMap();
            #endregion

            #region PaymentMethod

            CreateMap<PaymentMethod, PaymentMethodResDto>()
                .ForMember(dest => dest.paymentDetails, opt => opt.MapFrom((src, dest) =>
                    src.PaymentType == PaymentMethodType.VisaCard
                        ? $"Card: {src.CardNumber}, Holder: {src.CardHolderName}"
                        : (src.PaymentType == PaymentMethodType.VodafoneCash || src.PaymentType == PaymentMethodType.OrangeCash || src.PaymentType == PaymentMethodType.Instapay)
                            ? $"wallet number: {src.PhoneNumber}"
                            : (src.PaymentType == PaymentMethodType.Fawry)
                                ? $"Fawry Code: {src.FawryCode}"
                                : (src.PaymentType == PaymentMethodType.Cash)
                                    ? "Cash Payment"
                                    : string.Empty
                ))
                .ReverseMap();
            #endregion

            CreateMap<MessageResDto, Message>().ReverseMap();





            CreateMap<MessageCreateDto, Message>().ReverseMap();

            CreateMap<MessageUpdateDto, Message>().ReverseMap();
            CreateMap<Message, MessageReadDto>()
          .ForMember(dest => dest.SenderType, opt =>
           opt.MapFrom(src => src.CustomerId != null ? "Customer" : "Supplier"));



            CreateMap<Review, ReviewUpdateDto>().ReverseMap();
            CreateMap<Review, ReviewCreateDto>().ReverseMap();
            CreateMap<Review, ReviewResDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src=>src.Product.Name))
            .ReverseMap();


            CreateMap<WishlistResDto, Wishlist>().ReverseMap();

            CreateMap<NotificationResDto,Notification>().ReverseMap();

            CreateMap<NotificationCreateDto, Notification>().ReverseMap();

            CreateMap<NotificationUpdateDto, Notification>().ReverseMap();


            CreateMap<NotificationReadDto, Notification>().ReverseMap();



            CreateMap<CartResDto, Cart>().ReverseMap();
            CreateMap<CartItemResDto, CartItem>().ReverseMap();




            CreateMap<ReportResDto, Report>().ReverseMap();

        }

    }
}
