

namespace Horas.Api.Dtos.Message
{
    public class MessageUpdateDto
    {
        public Guid id { set; get; }

        public string Body { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        [ForeignKey("Supplier")]
        public Guid? SupplierId { get; set; }
    }
}
