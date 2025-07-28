namespace Horas.Api.Dtos.Review
{
    public class ReviewResDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string ProductName { get; set; }
        
    }
}
