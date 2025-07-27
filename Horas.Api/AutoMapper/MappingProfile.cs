using AutoMapper;
using Horas.Api.Dtos.AccountDto.Horas.Api.Dtos;
using Horas.Api.Dtos.Product;
using Horas.Api.Dtos.SubCategory;
using Horas.Domain;

namespace Horas.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductResDto, Product>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            //.ForMember(dest => dest.Rating, opt =>
            //    opt.MapFrom(src =>
            //    src.Reviews != null && src.Reviews.Any()
            //        ? src.Reviews.Sum(x => x.Rating) / (src.Reviews.Count * 5.0)
            //        : 0))
            //.ForMember(dest => dest.Rating, opt =>
            //    opt.MapFrom(src => src.Suppliers.Select(x => x.FactoryName)));
            #endregion


            #region SubCategory
            CreateMap<SubCategoryCreateDto, SubCategory>().ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<SubCategoryUpdateDto, SubCategory>().ReverseMap();
            CreateMap<SubCategoryResDto, SubCategory>().ReverseMap();
            #endregion
        }
    }
}
