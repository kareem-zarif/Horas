using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horas.Domain.Entities
{
    public class SellerProfile
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }

        public string StoreName { get; set; }
        public string BusinessType { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? StoreLogoUrl { get; set; }

        public virtual Person Person { get; set; }
    }
}
