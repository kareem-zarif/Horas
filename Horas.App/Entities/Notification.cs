namespace Horas.Domain
{
    public class Notification : BaseEnt
    {
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        //nav 
        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
}
