namespace Horas.Api.Dtos.SubCategory
{
    public class SubCategoryResDto 
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public List<ProductResDto> Products { get; set; }
    }
}
