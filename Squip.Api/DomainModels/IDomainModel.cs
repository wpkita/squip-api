using NodaTime;

namespace Squip.Api.DomainModels
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
