using AutoMapper;
using XtradesApi.Dtos;
using XtradesApi.Entities;

namespace XtradesApi.AutomapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Customer, CustomerData>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Customer, UpsertCustomer>()
               .ReverseMap();
        }
    }
}
