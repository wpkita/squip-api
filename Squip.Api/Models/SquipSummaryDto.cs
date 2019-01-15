using System.Collections.Generic;

namespace Squip.Api.Models
{
    public class SquipSummaryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
