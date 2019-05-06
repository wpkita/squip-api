using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.DomainModels;
using Squip.Api.Repositories;

namespace Squip.Api.Controllers
{
    [Route("api/idea")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]

    public class IdeaController
    {
        private readonly IRepository<Idea> ideaRepository;

        public IdeaController(IRepository<Idea> ideaRepository)
        {
            this.ideaRepository = ideaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Idea>> GetIdeas()
        {
            var ideas = await ideaRepository.GetAll();

            return ideas;
        }
    }
}
