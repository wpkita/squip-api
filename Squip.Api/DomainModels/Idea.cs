using System;
using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Firestore;

namespace Squip.Api.DomainModels
{
    public class Idea : DomainModelBase
    {
        public virtual string Content { get; set; }
        public virtual IEnumerable<string> Tags { get; set; }
        public virtual string UserId { get; set; }
    }
}
