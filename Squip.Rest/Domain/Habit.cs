namespace Squip.Rest.Domain;

public class Habit : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public bool IsArchived { get; set; }
}
