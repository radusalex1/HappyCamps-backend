using AutoMapper;
using HappyCamps_backend.Common;
using HappyCamps_backend.DTOs;
using HappyCamps_backend.Models;

namespace HappyCamps_backend.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.RoleType, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)));
        }
    }
}
