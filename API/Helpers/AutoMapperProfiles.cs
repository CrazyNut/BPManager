using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap<AppUser, MemberDTO>()
            // .ForMember(
            //     dest => dest.PhotoUrl, 
            //     opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url)
            // )
            // .ForMember(
            //     dest => dest.Age, 
            //     opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
            // );
            // CreateMap<Photo, PhotoDTO>();
            // CreateMap<MemberUpdateDTO, AppUser>();
        }
    }
}