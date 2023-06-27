using System.Collections.Generic;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Habits.Domain;

public class Habit : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public bool IsArchived { get; set; }
    public ICollection<Hibit> Hibits { get; set; }
}
