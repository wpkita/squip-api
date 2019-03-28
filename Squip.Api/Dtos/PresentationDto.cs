using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Squip.Api.Dtos
{
    public class PresentationDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
