using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SquipApi.Pocos;
using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SquipApi.Identity
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetCurrentUser()
        {
            var thirdPartyUserId = GetThirdPartyUserId();

            if (thirdPartyUserId == null)
            {
                throw new Exception("No user logged in!");
            }

            var squipUser = await GetOrCreateUser(thirdPartyUserId);

            if (squipUser == null)
            {
                throw new Exception("Something funky happened with the third party auth!");
            }

            return squipUser;
        }

        private async Task<User> GetOrCreateUser(string thirdPartyUserId)
        {
            return await GetUserByThirdPartyId(thirdPartyUserId) ??
                   await SynchronizeFirstTimeUser(thirdPartyUserId);
        }

        private string GetThirdPartyUserId()
        {
            return _httpContextAccessor?.HttpContext?.User?.Claims
                ?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        private async Task<User> SynchronizeFirstTimeUser(string thirdPartyUserId)
        {
            var accessToken = GetAccessToken();

            // If we have an access_token, then retrieve the user's information
            if (!string.IsNullOrEmpty(accessToken))
            {
                var apiClient = new AuthenticationApiClient(_configuration["Auth0:NakedDomain"]);
                var userInfo = await apiClient.GetUserInfoAsync(accessToken);

                var squipUser = new User
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email,
                    ThirdPartyId = thirdPartyUserId,
                    Picture = userInfo.Picture
                };

                await _context.Users.AddAsync(squipUser);
                await _context.SaveChangesAsync();

                return squipUser;
            }

            return null;
        }

        private string GetAccessToken()
        {
            return _httpContextAccessor.HttpContext.User?.Claims
                ?.FirstOrDefault(c => c.Type == "access_token")?.Value;
        }

        private async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        private async Task<User> GetUserByThirdPartyId(string thirdPartyId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ThirdPartyId == thirdPartyId);
        }
    }
}
