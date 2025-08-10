using Horas.Api.Dtos.Product;
using Horas.Domain;

namespace Horas.Api.Dtos.WishList
{
   
    public class UpdateWishlistDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
          
    }
    

}
