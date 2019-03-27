using System.Collections.Generic;

namespace Squip.Api.Dtos
{
    public class IdeaDto
    {
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
