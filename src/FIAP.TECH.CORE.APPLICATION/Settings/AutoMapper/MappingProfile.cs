using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.APPLICATION.Settings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Contact, ContactDetailsDto>();
        CreateMap<Region, RegionDetailsDto>();
    }
}
