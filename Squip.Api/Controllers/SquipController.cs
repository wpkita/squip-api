using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Api.Models;
using Squip.EntityFramework.Repositories;
using Squip.Pocos;

namespace Squip.Api.Controllers
{
    [Route("api/squips")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipController : ControllerBase
    {
        private readonly ISquipRepository _squipRepository;
        private readonly IMapper _mapper;
        public SquipController(IMapper mapper, ISquipRepository squipRepository)
        {
            _mapper = mapper;
            _squipRepository = squipRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SquipDto>> GetSquips()
        {
            var squipPocos = await _squipRepository.GetMostRecentSquipsAsync();
            var squipDtos = _mapper.Map<IEnumerable<SquipPoco>, IEnumerable<SquipDto>>(squipPocos);

            return squipDtos;
        }
    }
}
