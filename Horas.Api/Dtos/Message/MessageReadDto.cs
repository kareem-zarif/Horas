

namespace Horas.Api.Dtos.Message
{
    public class MessageReadDto
    {

        public Guid id { set; get; }

        public string Body { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        [ForeignKey("Supplier")]
        public Guid? SupplierId { get; set; }

        public string SenderType { get; set; }
    }
}
