using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Heplers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest=> dest.PhotoUrl, opt=> opt.MapFrom(src=> src.Photos.FirstOrDefault(x=> x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}