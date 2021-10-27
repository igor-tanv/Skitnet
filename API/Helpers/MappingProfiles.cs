using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<Product, ProductToReturnDto>()
      //destination, options, source (d, o ,s)
        .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
        .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
        .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
      CreateMap<Address, AddressDto>().ReverseMap();
      CreateMap<BasketItem, BasketItemDto>().ReverseMap();
      CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
      CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
    }
  }
}