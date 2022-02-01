using AutoMapper;
using Newtonsoft.Json;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using System.Collections.Generic;

namespace ProiectDAW.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<DirectSigninUserDTO, User>()
                .ForMember(user => user.DirectLoginUser, opt => opt.MapFrom(src => new DirectLoginUser { PasswordHash = BCrypt.Net.BCrypt.HashPassword(src.Password) }))
                .ForMember(user => user.UserSettings, opt => opt.MapFrom(src => UserSettings.GetDefaultSettings()))
                .ForMember(user => user.UserRole, opt => opt.MapFrom(src => "User"));

            CreateMap<BookUploadsRequestDTO, Book>()
                .ForMember(book => book.Genres, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ICollection<Genre>>(src.Genres)));
            CreateMap<Book, BookUploadsResponseDTO>();
            CreateMap<Book, BookDTO>();
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<Library, LibraryDTO>();
            CreateMap<LibraryBook, LibraryDTO>();
            CreateMap<UserSettings, UserSettingsDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, AdminEditableUserDTO>();
            CreateMap<Review, PostedReviewDTO>();
        }
    }
}
