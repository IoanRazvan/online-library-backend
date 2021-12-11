
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProiectDAW.Models;
using System;
using System.Collections.Generic;

namespace ProiectDAW.Security.Attributes
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ICollection<string> _roles;

        public AuthorizationAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusCodeObject = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            var user = (User)context.HttpContext.Items["User"];
            if (user == null || (_roles.Count != 0 && !_roles.Contains(user.UserRole)))
                context.Result = unauthorizedStatusCodeObject;
        }
    }
}
