using AutoMapper;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Services.Types;
using ProiectDAW.Utilities.JWT;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IJWTUtils _jwtUtilities;

        public UserService(IUserRepository repo, IMapper mapper, IJWTUtils jwtUtilities) : base(repo)
        {
            _mapper = mapper;
            _jwtUtilities = jwtUtilities;
        }

        public async Task<AuthenticationResult> Authenticate(DirectLoginUserDTO userDTO)
        {
            var user = await ((IUserRepository)_repo).FindByEmail(userDTO.Email);
            var errorObject = new { Message = "Bad Credentials!" };
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.DirectLoginUser.PasswordHash))
                return new AuthenticationResult { IsError = true, Error = errorObject };
            return new AuthenticationResult { IsError = false, Token = _jwtUtilities.GenerateJWTToken(user) };
        }

        public async Task<AuthenticationResult> Register(DirectSigninUserDTO userDTO)
        {
            if (await ((IUserRepository)_repo).ExistsByEmail(userDTO.Email))
                return new AuthenticationResult { IsError = true, Error = new { Message = "Email is already registered within our app!" } };
            User user = _mapper.Map<User>(userDTO);
            var created = await Create(user);
            if (!created)
                return new AuthenticationResult { IsError = true, Error = new { Message = "User could not be created" } };
            return new AuthenticationResult { IsError = false, Token = _jwtUtilities.GenerateJWTToken(user) };
        }
    }
}
