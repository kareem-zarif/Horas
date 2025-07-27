

namespace Horas.Api.Dtos.SubCategory
{
    public class SubCategoryCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
    }
}
