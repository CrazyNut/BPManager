using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ProcessExecutor.Services;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            Dictionary<string, string> types = ProcessElementTypesService.GetTypes();
            CreateMap<ProcessElementEntity, ProcessElementDTO>()
                 .ForMember(
                    dest => dest.ProcessElementInstanseType,
                    opt => opt.MapFrom(src => src.ProcessElementInstanseType.Name)
                 );
            CreateMap<ProcessElementDTO,ProcessElementEntity>()
                 .ForMember(
                    dest => dest.ProcessElementInstanseType,
                    opt => opt.MapFrom(src => Type.GetType(types[src.ProcessElementInstanseType]))
                 );
            CreateMap<ProcessEntity, ProcessDTO>();
            CreateMap<ProcessDTO, ProcessEntity>()
                .ForMember(
                dest => dest.ProcessElementsConnections,
                src => src.Ignore()
                )
                .ForMember(
                dest => dest.ProcessElements,
                src => src.Ignore()
                );
            CreateMap<ProcessElementConnectionEntity, ProcessElementConnectionDTO>().ReverseMap();
            CreateMap<ProcessParamEntity, ProcessParamDTO>().ReverseMap();
            // CreateMap<MemberUpdateDTO, AppUser>();
        }
    }
}