using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Domain;
using Squip.Services;

namespace Squip.Api.Controllers.Admin
{
    [Route("api/ideaImport")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    // [Authorize]

    public class IdeaImportController : ControllerBase
    {
        private readonly ISquipService squipService;

        public IdeaImportController(
            ISquipService squipService)
        {
            this.squipService = squipService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IEnumerable<dynamic> ideasToImport)
        {
            var firstSeveralIdeasAsDynamicJson = ideasToImport.Take(100);

            if (firstSeveralIdeasAsDynamicJson == null)
            {
                return BadRequest();
            }

            var ideas = new Collection<Idea>();
            foreach (var ideaJson in firstSeveralIdeasAsDynamicJson)
            {
                var idea = new Idea
                {
                    Content = $"# {ideaJson.title}\n![alt text]({ideaJson.media} \"{ideaJson.title}\")"
                };

                await squipService.Ideate(idea);

                ideas.Add(idea);
            }

            return Ok(ideas);
        }
    }
}
