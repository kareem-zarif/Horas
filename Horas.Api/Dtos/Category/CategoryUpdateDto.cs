

namespace Horas.Api.Dtos.Category
{
    public class CategoryUpdateDto 
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
    }
}
