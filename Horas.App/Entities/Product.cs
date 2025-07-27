using Horas.Domain.Entities;

namespace Horas.Domain
{
    public class Product : BaseEnt
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double PricePerPiece { get; set; }
        public double? PricePer50Piece { get; set; }
        public double? PricePer100Piece { get; set; }
        public int NoINStock { get; set; }
        public int MinNumToFactoryOrder { get; set; }
        public ProductApprovalStatus ApprovalStatus { get; set; } = ProductApprovalStatus.Pending;
        public IList<string> ProductPicsPathes { get; set; } = new List<string>();
        //extra details 
        public int? WarrantyNMonths { get; set; }
        public ShippingTypes Shipping { get; set; } = ShippingTypes.None;
        public Guid SubCategoryId { get; set; }
        //nav
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new HashSet<ProductSupplier>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public virtual ICollection<ProductWishList> ProductWishLists { get; set; } = new HashSet<ProductWishList>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
