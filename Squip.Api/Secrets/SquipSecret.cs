using System;
using System.Collections.Generic;
using System.Linq;

namespace Squip.Api.Secrets
{
    public class SquipSecret
    {
        public SquipSecret(IDictionary<string, object> dict)
        {
            Id = dict.TryGetValue("id", out var id) ? id.ToString() : null;
            Content = dict.TryGetValue("content", out var content) ? content.ToString() : null;
            Tags = dict.TryGetValue("tags", out var tags) ? ((tags as IEnumerable<object>) ?? new string[0]).Cast<string>() : null;
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
