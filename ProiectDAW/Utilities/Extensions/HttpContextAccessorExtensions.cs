using Microsoft.AspNetCore.Http;
using ProiectDAW.Models;

namespace ProiectDAW.Utilities.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static User GetPrincipal(this IHttpContextAccessor httpContextAccessor)
        {
            return (User)httpContextAccessor.HttpContext.Items["User"];
        }
    }
}
