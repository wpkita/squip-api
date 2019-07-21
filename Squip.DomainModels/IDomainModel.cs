using NodaTime;

namespace Squip.Domain
{
    public interface IDomainModel
    {
        string Id { get; set; }
        Instant InstantCreatedAt { get; set; }
        Instant InstantUpdatedAt { get; set; }
        void PreCreate();
        void PreUpdate();
    }
}
