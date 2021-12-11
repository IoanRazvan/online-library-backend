using AutoMapper;
using ProiectDAW.DTOs;
using ProiectDAW.Models;


namespace ProiectDAW.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<DirectSigninUserDTO, User>()
                .ForMember(user => user.DirectLoginUser, opt => opt.MapFrom(src => new DirectLoginUser { PasswordHash = BCrypt.Net.BCrypt.HashPassword(src.Password) }))
                .ForMember(user => user.UserSettings, opt => opt.MapFrom(src => UserSettings.DEFAULT_SETTINGS))
                .ForMember(user => user.UserRole, opt => opt.MapFrom(src => "User"));
        }
    }
}
