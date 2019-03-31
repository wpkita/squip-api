using NodaTime;

namespace Squip.Api.DomainModels
{
    public abstract class DomainModelBase : IDomainModel
    {
        public Instant InstantCreatedAt { get; set; }

        public void PreCreate()
        {
            InstantCreatedAt = SystemClock.Instance.GetCurrentInstant();
        }
    }
}

