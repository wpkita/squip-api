using System;

namespace Squip.Rest.Domain;

public class Tag : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public Guid IdeaId { get; set; }
    public Idea Idea { get; set; }
    public bool IsArchived { get; set; }
}
