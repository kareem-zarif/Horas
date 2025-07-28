
namespace Horas.Domain
{
    public class Customer : Person
    {
        //nav
        public virtual Cart Cart { get; set; }
        public virtual Wishlist Wishlist { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new HashSet<PaymentMethod>();

        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

    }
}
