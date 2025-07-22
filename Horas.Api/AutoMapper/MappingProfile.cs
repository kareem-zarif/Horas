using AutoMapper;
using Horas.Api.Dtos.Product;
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
            CreateMap<ProductResDto, Product>().ReverseMap()
                .ForMember(dest => dest.Rating, opt =>
                    opt.MapFrom(src =>
                    src.Reviews != null && src.Reviews.Any()
                        ? src.Reviews.Sum(x => x.Rating) / (src.Reviews.Count * 5.0)
                        : 0))
                .ForMember(dest => dest.Rating, opt =>
                    opt.MapFrom(src => src.Suppliers.Select(x => x.FactoryName)));
            #endregion

        }
    }
}
