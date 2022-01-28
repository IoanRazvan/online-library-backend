using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Utilities.Extensions;
using ProiectDAW.Utilities.JWT;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IJWTUtils _jwtUtilities;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IUserRepository repo, IMapper mapper, IJWTUtils jwtUtilities, IHttpContextAccessor httpContext) : base(repo)
        {
            _mapper = mapper;
            _jwtUtilities = jwtUtilities;
            _httpContext = httpContext;
        }

        public async Task<string> Authenticate(DirectLoginUserDTO userDTO)
        {
            var user = await ((IUserRepository)_repo).FindByEmail(userDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.DirectLoginUser.PasswordHash))
                return null;
            return _jwtUtilities.GenerateJWTToken(user);
        }

        public async Task<string> Create(DirectSigninUserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            var created = await Create(user);
            if (!created)
                return null;
            return _jwtUtilities.GenerateJWTToken(user);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await ((IUserRepository)_repo).ExistsByEmail(email);
        }

        public UserDTO Find()
        {
            return _mapper.Map<UserDTO>(_httpContext.GetPrincipal());
        }

        public async Task<bool> Update(UserDTO userInformation)
        {
            User principal = _httpContext.GetPrincipal();
            principal.FirstName = userInformation.FirstName;
            principal.LastName = userInformation.LastName;
            principal.Email = userInformation.Email;
            return await base.Update(principal);
        }
    }
}
