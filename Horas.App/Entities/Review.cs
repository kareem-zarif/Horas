namespace Horas.Domain
{
    public class Review : BaseEnt
    {
        //supplier review calculated dynamically (Avg of his products review)
        public int Rating { get; set; }
        public string? Comment { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
