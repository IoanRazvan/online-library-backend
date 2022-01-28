using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class UserSettingsService : GenericService<UserSettings>, IUserSettingsService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserSettingsService(IUserSettingsRepository repo, IHttpContextAccessor httpContext) : base(repo)
        {
            _httpContext = httpContext;
        }

        public async Task<bool> Update(UserSettingsDTO userSettings)
        {
            User u = _httpContext.GetPrincipal();
            u.UserSettings.NumberOfRecentBooks = userSettings.NumberOfRecentBooks;
            u.UserSettings.RememberPageNumber = userSettings.RememberPageNumber;
            return await base.Update(u.UserSettings);
        }
    }
}
