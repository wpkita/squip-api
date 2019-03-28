using System.ComponentModel.DataAnnotations;

namespace Squip.Api.Dtos
{
    public class ReactionDto
    {
        [Required]
        public string PresentationId { get; set; }

        [Required]
        public string ReactionCategory { get; set; }
    }
}
