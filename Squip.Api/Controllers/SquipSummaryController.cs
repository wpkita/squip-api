using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.Models;
using Squip.EntityFramework.Repositories;
using Squip.Pocos;

namespace Squip.Api.Controllers
{
    [Route("api/squipsummaries")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipSummaryController
    {
        private readonly ISquipRepository _squipRepository;
        private readonly IMapper _mapper;
        public SquipSummaryController(IMapper mapper, ISquipRepository squipRepository)
        {
            _mapper = mapper;
            _squipRepository = squipRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SquipSummaryDto>> GetSquipSummaries()
        {
            var squipPocos = await _squipRepository.GetMostRecentSquipsAsync();
            var squipSummaryDtos = _mapper.Map<IEnumerable<SquipPoco>, IEnumerable<SquipSummaryDto>>(squipPocos);

            return squipSummaryDtos;
        }
    }
}
