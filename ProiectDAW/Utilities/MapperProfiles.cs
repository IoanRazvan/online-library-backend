using AutoMapper;
using ProiectDAW.DTOs;
using ProiectDAW.Models;


namespace ProiectDAW.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<DirectLoginUserDTO, User>()
                .ForMember(user => user.DirectLoginUser, opt => opt.MapFrom(src => new DirectLoginUser { PasswordHash = src.Password }))
                .ForMember(user => user.UserSettings, opt => opt.MapFrom(src => UserSettings.DEFAULT_SETTINGS));
        }
    }
}
