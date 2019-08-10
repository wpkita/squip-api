using System.Collections.Generic;

namespace Squip.Api.Dtos
{
    public class PresentationDto
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
