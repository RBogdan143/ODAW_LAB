using AutoMapper;
using Lab6.Models;
using Lab6.Models.DTOs;

namespace Lab6.Helpers
{
    public class MapperProfile: Profile
    {
        public MapperProfile() {

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>()
                .ForMember( ud => ud.FullName,
                opts => opts.MapFrom(u => u.FirstName + u.LastName));
        }
    }
}
