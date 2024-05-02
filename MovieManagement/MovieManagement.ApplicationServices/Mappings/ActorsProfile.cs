﻿using AutoMapper;
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
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName));

        CreateMap<AddActorRequest, DataAccess.Entities.Actor>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName));

        CreateMap<DataAccess.Entities.Actor, Actor>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName));
    }
}
