namespace Horas.Api.Dtos.AccountDto
{
    public class SupplierWithProfileDto
    {
        public Guid Id { get; set; } // Supplier Id
        public string? SupplierName { get; set; }

        // بيانات من الـ Person
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // بيانات من الـ SellerProfile
        public string? StoreName { get; set; }
        public string? BusinessType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? StoreLogoUrl { get; set; }
    }
}
