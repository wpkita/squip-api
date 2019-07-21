using System.Threading.Tasks;
using AutoMapper;
using Squip.Data;
using Squip.Domain;

namespace Squip.Services
{
    public class SquipService : ISquipService
    {
        private readonly ISquipRepository _squipRepository;
        private readonly IRepository<Idea> _ideaRepository;
        private readonly IRepository<Reaction> _reactionRepository;
        private readonly IRepository<Presentation> _presentationRepository;

        public SquipService(
            ISquipRepository squipRepository,
            IRepository<Idea> ideaRepository,
            IRepository<Reaction> reactionRepository,
            IRepository<Presentation> presentationRepository)
        {
            _squipRepository = squipRepository;
            _ideaRepository = ideaRepository;
            _reactionRepository = reactionRepository;
            _presentationRepository = presentationRepository;
        }

        public async Task Ideate(Idea idea)
        {
            idea.Id = _squipRepository.GetNextIdeaId();
            await _ideaRepository.Create(idea);
        }

        public async Task<Presentation> Inquire(Inquiry inquiry)
        {
            // Get random idea
            var randomIdeaId = await _squipRepository.GetRandomIdeaId();
            var idea = await _ideaRepository.GetById(randomIdeaId);

            // Build presentation
            var presentation = Mapper.Map<Presentation>(idea);
            presentation.Id = _squipRepository.GetNextPresentationId();
            presentation.UserId = inquiry.UserId;
            presentation.PreCreate();
            presentation = await _presentationRepository.Create(presentation);

            return presentation;
        }

        public async Task<Presentation> React(Reaction reaction)
        {
            // Create reaction
            reaction.PreCreate();
            reaction.Id = _squipRepository.GetNextReactionId();
            reaction = await _reactionRepository.Create(reaction);

            // Get next presentation
            var inquiry = new Inquiry { UserId = reaction.UserId };
            var presentation = await Inquire(inquiry);

            return presentation;
        }
    }
}
