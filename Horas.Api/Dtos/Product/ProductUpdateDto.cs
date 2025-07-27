
namespace Horas.Api.Dtos.Product
{
    public class ProductUpdateDto 
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        public double PricePerPiece { get; set; }
        public double? PricePer50Piece { get; set; }
        public double? PricePer100Piece { get; set; }
        public int NoINStock { get; set; }
        public int MinNumToFactoryOrder { get; set; }
        public ProductApprovalStatus ApprovalStatus { get; set; } = ProductApprovalStatus.Pending;

        public List<IFormFile>? Images { get; set; } // Images are optional in update, so it can be null

        //extra details 
        public int? WarrantyNMonths { get; set; }
        public ShippingTypes Shipping { get; set; } = ShippingTypes.None;
        public Guid SubCategoryId { get; set; }
    }
}
