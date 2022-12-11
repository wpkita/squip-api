using System.Collections.Generic;

namespace Squip.Rest.Domain;

public class Mood : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public bool IsArchived { get; set; }
    public ICollection<MoodEntry> MoodEntries { get; set; }
}
