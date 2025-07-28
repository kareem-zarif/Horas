namespace Horas.Api.Dtos.SubCategory
{
    public class SubCategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
