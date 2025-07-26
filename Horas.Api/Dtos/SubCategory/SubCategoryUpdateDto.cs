namespace Horas.Api.Dtos.SubCategory
{
    public class SubCategoryUpdateDto 
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
