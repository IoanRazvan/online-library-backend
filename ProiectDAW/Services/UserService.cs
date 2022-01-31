using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Services.Types;
using ProiectDAW.Utilities.Extensions;
using ProiectDAW.Utilities.JWT;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IJWTUtils _jwtUtilities;
        private readonly IHttpContextAccessor _httpContext;

        public int BAD_CREDENTIALS => 1;

        public int DISABLED_ACCOUNT => 2;

        public UserService(IUserRepository repo, IMapper mapper, IJWTUtils jwtUtilities, IHttpContextAccessor httpContext) : base(repo)
        {
            _mapper = mapper;
            _jwtUtilities = jwtUtilities;
            _httpContext = httpContext;
        }

        public async Task<AuthenticationResult> Authenticate(DirectLoginUserDTO userDTO)
        {
            var user = await ((IUserRepository)_repo).FindByEmail(userDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.DirectLoginUser.PasswordHash))
                return new AuthenticationResult(BAD_CREDENTIALS);
            if (user.IsDisabled)
                return new AuthenticationResult(DISABLED_ACCOUNT);
            return new AuthenticationResult(_jwtUtilities.GenerateJWTToken(user));
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

        public async Task<Page<AdminEditableUserDTO>> FindAllExceptPrincipalPaged(int page, int pageSize, string q)
        {
            var userId = _httpContext.GetPrincipal().Id;
            var result = await ((IUserRepository)_repo).FindByPredicatePaged(HasNameAndNotId(q, userId), page, pageSize);
            var totalUsers = await ((IUserRepository)_repo).CountByPredicate(HasNameAndNotId(q, userId));
            return new Page<AdminEditableUserDTO>
            {
                CurrentPageNumber = page,
                LastPageNumber = PageUtils.ComputeLastPage(totalUsers, pageSize),
                Result = _mapper.Map<List<User>, List<AdminEditableUserDTO>>(result),
                Query = q,
                PageSize = pageSize
            };
        }

        private static Expression<Func<User, bool>> HasNameAndNotId(string name, Guid id)
        {
            return user => !user.Id.Equals(id) && (user.FirstName + " " + user.LastName).ToLower().Contains(name.ToLower());
        }

        public async Task<bool> Update(UserDTO userInformation)
        {
            User principal = _httpContext.GetPrincipal();
            principal.FirstName = userInformation.FirstName;
            principal.LastName = userInformation.LastName;
            principal.Email = userInformation.Email;
            return await base.Update(principal);
        }

        public bool IsPrincipal(User user)
        {
            return user.Id.Equals(_httpContext.GetPrincipal().Id);
        }

        public async Task<AdminEditableUserDTO> PromoteUser(User user)
        {
            user.UserRole = "Admin";
            return await UpdateAndMap(user);
        }

        public async Task<AdminEditableUserDTO> EnableUser(User user)
        {
            user.IsDisabled = false;
            return await UpdateAndMap(user);
        }

        private async Task<AdminEditableUserDTO> UpdateAndMap(User user)
        {
            if (!await base.Update(user))
                return null;
            return _mapper.Map<AdminEditableUserDTO>(user);
        }

        public async Task<AdminEditableUserDTO> DisableUser(User user)
        {
            user.IsDisabled = true;
            return await UpdateAndMap(user);
        }
    }
}
