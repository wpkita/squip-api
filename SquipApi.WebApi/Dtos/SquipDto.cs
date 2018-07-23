using System.Collections.Generic;

namespace SquipApi.WebApi.Dtos
{
    public class SquipDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public IList<string> Tags { get; set; }
    }
}