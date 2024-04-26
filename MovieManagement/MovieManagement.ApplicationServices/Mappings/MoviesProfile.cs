﻿using AutoMapper;
using MovieManagement.ApplicationServices.API.Domain.Models;

namespace MovieManagement.ApplicationServices.Mappings;

public class MoviesProfile : Profile
{
    public MoviesProfile()
    {
        CreateMap<DataAccess.Entities.Movie, Movie>()
            .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            .ForMember(x => x.Year, y => y.MapFrom(z => z.Year))
            .ForMember(x => x.Universe, y => y.MapFrom(z => z.Universe))
            .ForMember(x => x.BoxOffice, y => y.MapFrom(z => z.BoxOffice));
    }
}