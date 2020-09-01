using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
             .ForMember(dest => dest.ProductType, source =>
                    source.MapFrom(src => src.ProductType.Name))
             .ForMember(dest => dest.ProductBrand, source =>
                    source.MapFrom(src => src.ProductBrand.Name))
             .ForMember(dest => dest.PictureUrl, source =>
                    source.MapFrom<ProductUrlResolver>());
        }
    }
}