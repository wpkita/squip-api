using NodaTime;

namespace Squip.Api.DomainModels
{
    public interface IDomainModel
    {
        string Id { get; set; }
        Instant InstantCreatedAt { get; set; }
        void PreCreate();
    }
}
