using AutoMapper;
using Domain.Entities;
using Domain.Data;

namespace Domain.Profiles;

public class RouteMappingProfile : Profile
{
    public RouteMappingProfile()
    {
        CreateMap<RouteRecord, RouteDto>()
            .ForMember(dto => dto.LocationName, opt => opt.MapFrom(record => record.RoutePartName));
    }
}
