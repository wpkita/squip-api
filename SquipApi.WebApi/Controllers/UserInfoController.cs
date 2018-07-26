using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquipApi.EntityFramework;
using SquipApi.Pocos;
using SquipApi.WebApi.Dtos;
using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SquipApi.Identity;

namespace SquipApi.WebApi.Controllers
{
    public class UserInfoController : BaseController
    {
        private readonly IUserService _userService;

        public UserInfoController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<User> Get()
        {
            var user = await _userService.GetCurrentUser();

            return user;
        }
    }
}
