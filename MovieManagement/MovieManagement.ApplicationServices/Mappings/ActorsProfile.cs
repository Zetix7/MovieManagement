using AutoMapper;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;

namespace MovieManagement.ApplicationServices.Mappings;

public class ActorsProfile : Profile
{
    public ActorsProfile()
    {
        CreateMap<UpdateActorByIdRequest, DataAccess.Entities.Actor>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.Movies, y => y.MapFrom(z => z.MovieListIds));

        CreateMap<int, DataAccess.Entities.Movie>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z));

        CreateMap<AddActorRequest, DataAccess.Entities.Actor>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.Movies, y => y.MapFrom(z => z.MovieListIds));

        CreateMap<DataAccess.Entities.Actor, Actor>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.MovieTitleList, y => y.MapFrom(z => z.Movies!.Select(m => new string($"{m.Title} ({m.Year})"))));
    }
}
