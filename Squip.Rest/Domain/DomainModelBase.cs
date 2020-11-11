using System;
using NodaTime;

namespace Squip.Rest.Domain
{
    public abstract class DomainModelBase : IDomainModel
    {
        public string Id { get; set; }
        public Instant InstantCreatedAt { get; set; }
        public Instant InstantUpdatedAt { get; set; }

        public void PreCreate()
        {
            Id ??= Guid.NewGuid().ToString();

            InstantCreatedAt = SystemClock.Instance.GetCurrentInstant();
        }

        public void PreUpdate()
        {
            InstantUpdatedAt = SystemClock.Instance.GetCurrentInstant();
        }
    }
}
