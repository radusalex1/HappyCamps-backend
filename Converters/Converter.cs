using AutoMapper;
using HappyCamps_backend.DTOs;
using HappyCamps_backend.Mapper;
using HappyCamps_backend.Models;

namespace HappyCamps_backend.Converters
{
    public class Converter
    {
        private readonly IMapper _mapper;

        public Converter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public User ToUser(UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<User>(userRegisterDTO);

            user.Points = 0;

            user.Accepted = user.RoleType == Common.Role.VOLUNTEER ? true : false;

            return user;
        }
    }
}
