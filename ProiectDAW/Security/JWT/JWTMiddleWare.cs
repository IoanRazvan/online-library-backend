
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProiectDAW.Services;
using ProiectDAW.Utilities.JWT;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Utilities
{
    public class JWTMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JWTMiddleWare(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService, IJWTUtils jWTUtilities)
        {
            string token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jWTUtilities.ValidateJWTToken(token);

            if (userId != Guid.Empty)
            {
                httpContext.Items["User"] = await userService.Find(userId);
            }

            await _next(httpContext);
        }
    }
}
