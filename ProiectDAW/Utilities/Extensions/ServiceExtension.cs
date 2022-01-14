﻿using Microsoft.Extensions.DependencyInjection;
using ProiectDAW.Repositories;
using ProiectDAW.Services;
using ProiectDAW.Utilities.JWT;

namespace ProiectDAW.Utilities
{
    public static class ServiceExtension
    {
        public static IServiceCollection  AddService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IGenreService, GenreService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            return services;
        }

        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddTransient<IJWTUtils, JWTUtils>();
            return services;
        }
    }
}