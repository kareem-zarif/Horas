using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Horas.Data
{
    //public class HorasDBContext: IdentityDbContext<Person>
    public class HorasDBContext : IdentityDbContext<
    Person,
    Role,
    Guid,
    UserClaim,
    UserRole,
    UserLogin,
    RoleClaim,
    UserToken>

    {
        public HorasDBContext(DbContextOptions<HorasDBContext> options) : base(options)
        {
        }

        //onconfiguring => IOC

        //onmodeling => EntiteConfi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    //entry.Property(x => x.CreatedOn).IsModified = false; //not track CreatedOn in case of modifing 
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        #region Why virtual
        //virtual for =>Diff derived contexts: testing/mocking/productoion
        //+ LazyLoading
        #endregion
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        //public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        public virtual DbSet<ProductWishList> ProductWishLists { get; set; }
        public virtual DbSet<PersonNotification> PersonNotification { get; set; }

        public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }

    }

    public class Role : IdentityRole<Guid> { }
    public class UserClaim : IdentityUserClaim<Guid> { }
    public class UserLogin : IdentityUserLogin<Guid> { }
    public class UserToken : IdentityUserToken<Guid> { }
    public class RoleClaim : IdentityRoleClaim<Guid> { }
    public class UserRole : IdentityUserRole<Guid> { }

}
