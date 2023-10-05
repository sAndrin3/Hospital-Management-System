using Authentication.Models;
using Authentication.Models.Dto;
using AutoMapper;

namespace Authentication.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterDto, ApplicationUser>().ForMember(dest=>dest.UserName, u=>u.MapFrom(reg=>reg.Email));
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
