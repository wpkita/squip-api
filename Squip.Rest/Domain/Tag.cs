using System;

namespace Squip.Rest.Domain
{
    public class Tag : DomainModelBase
    {
        public string Name { get; set; }
        public Guid IdeaId { get; set; }
        public Idea Idea { get; set; }
    }
}
