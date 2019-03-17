using System;
using System.Threading.Tasks;
using Squip.Api.Dtos;
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
        public async Task<PresentationDto> Present()
        {
            var squip = await _squipRepository.GetSquip();
            var presentationSecret = new PresentationSecret
            {
                SquipId = squip.Id
            };

            await _squipRepository.AddPresentation(presentationSecret);

            return new PresentationDto
            {
                Id = presentationSecret.Id,
                Content = squip.Content
            };
        }

        public async Task<PresentationDto> ProcessReactionThenPresent(ReactionDto reactionDto)
        {
            var doesPresentationExist = await _squipRepository.DoesPresentationExist(reactionDto.PresentationId);
            if (!doesPresentationExist)
            {
                throw new Exception("NONE");
            }
            var reactionSecret = new ReactionSecret
            {
                PresentationId = reactionDto.PresentationId,
                ReactionCategory = reactionDto.ReactionCategory
            };

            await _squipRepository.AddReaction(reactionSecret);

            var presentation = await Present();

            return presentation;
        }
    }
}
