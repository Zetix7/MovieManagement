using AutoMapper;
using MovieManagement.ApplicationServices.API.Domain.Models;

namespace MovieManagement.ApplicationServices.Mappings;

public class ActorsProfile : Profile
{
    public ActorsProfile()
    {
        CreateMap<DataAccess.Entities.Actor, Actor>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName));
    }
}
