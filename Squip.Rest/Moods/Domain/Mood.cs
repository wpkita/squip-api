using System.Collections.Generic;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Moods.Domain;

public class Mood : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public bool IsArchived { get; set; }
    public ICollection<MoodEntry> MoodEntries { get; set; }
}
