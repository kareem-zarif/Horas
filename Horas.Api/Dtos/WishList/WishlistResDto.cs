
namespace Horas.Api.Dtos.WishList
{
    public class WishlistResDto
    {
        public Guid Id { get; set; }
        //public Guid CustomerId { get; set; }
        //public string CustomerName { get; set; }
        //public string CustomerEmail { get; set; }
        // public List<Guid> ProductIds { get; set; } = new List<Guid>();
        // public List<string> ProductNames { get; set; } = new List<string>();
        //  public List<string> ProductImages { get; set; } = new List<string>();

        public List<ProductResDto> Products { get; set; } = new();
    }
}
