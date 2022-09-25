using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities.ProcessExecutor;
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
             CreateMap<ProcessElementSample, ProcessElementSampleDTO>()
             .ForMember(
                dest => dest.ProcessElementInstanseType,
                opt => opt.MapFrom(src => src.ProcessElementInstanseType.AssemblyQualifiedName)
             );
             CreateMap<ProcessElementSampleDTO,ProcessElementSample>()
             .ForMember(
                dest => dest.ProcessElementInstanseType,
                opt => opt.MapFrom(src => Type.GetType(src.ProcessElementInstanseType))
             );
             CreateMap<ProcessSample, ProcessSampleDTO>().ReverseMap();
             CreateMap<ProcessElementConnection, ProcessElementConnectionDTO>().ReverseMap();
             CreateMap<ProcessParam, ProcessParamDTO>().ReverseMap();
            // CreateMap<MemberUpdateDTO, AppUser>();
        }
    }
}