using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Squip.Api.Dtos
{
    public class IdeaDto
    {
        [Required]
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
