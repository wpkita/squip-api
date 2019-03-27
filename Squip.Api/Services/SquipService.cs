using System;
using System.Linq;
using System.Threading.Tasks;
using Squip.Api.Dtos;
using Squip.Api.Identity;
using Squip.Api.Repositories;
using Squip.Api.Secrets;

namespace Squip.Api.Services
{
    public class SquipService : ISquipService
    {
        private readonly ISquipRepository _squipRepository;

        public SquipService(ISquipRepository squipRepository)
        {
            this._squipRepository = squipRepository;
        }

        public async Task<ValidationDto> Ideate(IUser user, IdeaDto ideaDto)
        {
            var ideaSecret = new IdeaSecret
            {
                UserId = user.Id,
                Content = ideaDto.Content,
                Tags = ideaDto.Tags.ToArray()
            };

            await _squipRepository.AddIdea(ideaSecret);

            return new ValidationDto();
        }

        public async Task<PresentationDto> Present(IUser user)
        {
            var squip = await _squipRepository.GetIdea();

            var presentationSecret = new PresentationSecret
            {
                UserId = user?.Id,
                SquipId = squip.Id
            };

            presentationSecret = await _squipRepository.AddPresentation(presentationSecret);

            return new PresentationDto
            {
                Id = presentationSecret.Id,
                Content = squip.Content,
                Tags = squip.Tags
            };
        }

        public async Task<PresentationDto> React(IUser user, ReactionDto reactionDto)
        {
            var reactionSecret = new ReactionSecret
            {
                UserId = user.Id,
                PresentationId = reactionDto.PresentationId,
                ReactionCategory = reactionDto.ReactionCategory
            };

            await _squipRepository.AddReaction(reactionSecret);

            var presentation = await Present(user);

            return presentation;
        }
    }
}
