using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Squip.Api.DomainModels;
using Squip.Api.Dtos;
using Squip.Api.Identity;
using Squip.Api.Repositories;

namespace Squip.Api.Services
{
    public class SquipService : ISquipService
    {
        private readonly ISquipRepository _squipRepository;

        public SquipService(ISquipRepository squipRepository)
        {
            _squipRepository = squipRepository;
        }

        public async Task Ideate(Idea idea)
        {
            idea.PreCreate();
            idea.Id = _squipRepository.GetNextIdeaId();
            await _squipRepository.CreateIdea(idea);
        }

        public async Task<Presentation> Inquire(Inquiry inquiry)
        {
            // Get random idea
            var randomIdeaId = await _squipRepository.GetRandomIdeaId();
            var idea = await _squipRepository.GetIdea(randomIdeaId);

            // Build presentation
            var presentation = Mapper.Map<Presentation>(idea);
            presentation.Id = _squipRepository.GetNextPresentationId();
            presentation.UserId = inquiry.UserId;
            presentation.PreCreate();
            presentation = await _squipRepository.CreatePresentation(presentation);

            return presentation;
        }

        public async Task<Presentation> React(Reaction reaction)
        {
            // Create reaction
            reaction.PreCreate();
            reaction.Id = _squipRepository.GetNextReactionId();
            reaction = await _squipRepository.CreateReaction(reaction);

            // Get next presentation
            var inquiry = new Inquiry { UserId = reaction.UserId };
            var presentation = await Inquire(inquiry);

            return presentation;
        }
    }
}
