namespace Horas.Domain
{
    public class Review : IBaseEnt
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
        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
