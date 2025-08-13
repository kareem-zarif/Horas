

namespace Horas.Api.Dtos.Message
{
    public class MessageCreateDto
    {
        public string Body { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        [ForeignKey("Supplier")]
        public Guid? SupplierId { get; set; }
        public string SenderType { get; set; }

        public string Sendto { get; set; }
     
    }
}
