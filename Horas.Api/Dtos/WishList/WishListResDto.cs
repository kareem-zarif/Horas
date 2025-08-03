using System.Collections.ObjectModel;

namespace Horas.Api.Dtos.WishList
{
    public class WishListResDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public ICollection<ProductResDto> Products { get; set; }
    }
}
