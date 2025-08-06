using Address = Horas.Domain.Address;
using Customer = Horas.Domain.Customer;
using PaymentMethod = Horas.Domain.PaymentMethod;
using Product = Horas.Domain.Product;
using Review = Horas.Domain.Review;

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
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
            .ReverseMap();

        CreateMap<SupplierUpdateDto, Supplier>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
            .ReverseMap();

        CreateMap<Supplier, SupplierResDto>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src =>
             src.Addresses != null && src.Addresses.Any() ? src.Addresses.First().State : null))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src =>
             src.Addresses != null && src.Addresses.Any() ? src.Addresses.First().City : null))
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



        #region Message
        CreateMap<MessageResDto, Message>().ReverseMap();

        CreateMap<MessageCreateDto, Message>().ReverseMap();

        CreateMap<MessageUpdateDto, Message>().ReverseMap();
        CreateMap<Message, MessageReadDto>()
            .ForMember(dest => dest.SenderType, opt =>
             opt.MapFrom(src => src.CustomerId != null ? "Customer" : "Supplier"))
              .ForMember(dest => dest.MessageDateTime, opt => opt.MapFrom(src => src.CreatedOn))
              .ForMember(des => des.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? $"{src.Supplier.FirstName} {src.Supplier.LastName}" : null))
              .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : null))
            .ReverseMap();
        #endregion

        #region review
        CreateMap<Review, ReviewUpdateDto>().ReverseMap();
        CreateMap<Review, ReviewCreateDto>().ReverseMap();
        CreateMap<Review, ReviewResDto>()
        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
        .ReverseMap();
        #endregion

        #region notification
        CreateMap<NotificationResDto, Notification>().ReverseMap();
        CreateMap<NotificationCreateDto, Notification>().ReverseMap();
        CreateMap<NotificationUpdateDto, Notification>().ReverseMap();
        CreateMap<NotificationReadDto, Notification>().ReverseMap();
        #endregion


        CreateMap<ReportResDto, Report>().ReverseMap();


        #region Wishlist

        CreateMap<Wishlist, WishListCreateDto>()
            .ReverseMap();
        CreateMap<Wishlist, UpdateWishlistDto>()
            .ReverseMap();
        CreateMap<Wishlist, WishListResDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
            .ForMember(dest => dest.ProductWishlist, opt => opt.MapFrom(src => src.ProductWishLists.ToList()))
            .ReverseMap();

        #endregion

        #region ProductWishlist
        CreateMap<ProductWishList, ProductWishlistCreateDto>().ReverseMap();
        CreateMap<ProductWishlistUpdateDto, ProductWishList>().ReverseMap();
        CreateMap<ProductWishList, ProductWishlistResDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ReverseMap();

        #endregion

        #region Orders
        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<OrderUpdateDto, Order>().ReverseMap();
        CreateMap<Order, OrderResDto>()
                   .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : null))
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
            .ForMember(des => des.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn))
                 .ReverseMap();

        #endregion

        #region PaymentMethod

        CreateMap<PaymentMethodCreateDto, PaymentMethod>().ReverseMap();
        CreateMap<PaymentMethod, PaymentMethodUpdateDto>().ReverseMap();

        CreateMap<PaymentMethod, PaymentMethodResDto>()
            .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
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

    }

}

