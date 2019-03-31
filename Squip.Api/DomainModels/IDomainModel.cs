using NodaTime;

namespace Squip.Api.DomainModels
{
    public interface IDomainModel
    {
        Instant InstantCreatedAt { get; set; }
        void PreCreate();
    }
}
