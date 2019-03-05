using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.Repositories;
using Squip.Api.Models;

namespace Squip.Api.Controllers
{
    [Route("api/squip")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipController : ControllerBase
    {
        private readonly ISquipRepository _squipRepository;
        public SquipController(ISquipRepository squipRepository)
        {
            _squipRepository = squipRepository;
        }

        [HttpGet]
        public async Task<SquipDto> GetSquip()
        {
            var squip = await _squipRepository.GetSquip();

            return squip;
        }
    }
}
