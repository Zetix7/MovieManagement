﻿using AutoMapper;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;

namespace MovieManagement.ApplicationServices.Mappings;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<AddUserRequest, DataAccess.Entities.User>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.Login, y => y.MapFrom(z => z.Username))
            .ForMember(x => x.Password, y => y.MapFrom(z => z.Password));

        CreateMap<DataAccess.Entities.User, User>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.Username, y => y.MapFrom(z => z.Login));
    }
}
